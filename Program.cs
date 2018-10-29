/*

Discord message backup tool
Copyright (c) 2018 never_released

This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org/>

*/

using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.ComponentModel.DataAnnotations;

namespace DiscordBackup
{
    class Program
    {
        public async Task MainAsync(string[] args) {
            Console.WriteLine("Discord Message Backup Tool\nCopyright (c) 2018 never_released\n\n");
            DiscordSocketClient socketClient = new DiscordSocketClient();
            if (args.Length != 0) {
                Console.WriteLine("The number of arguments is incorrect.");
                return;
            }
            Console.Write("Token: ");
            string token = Console.ReadLine();
            await socketClient.LoginAsync(TokenType.Bot, token);
            await socketClient.StartAsync();
            if (socketClient.LoginState != LoginState.LoggedIn) {
                Console.WriteLine("Failed to log in.");
                return;
            }
            Console.Write("Channel ID (has to be a text channel): ");
            string channelIdString = Console.ReadLine();
            ulong channelId = ulong.Parse(channelIdString);
            SocketTextChannel channel = (SocketTextChannel) socketClient.GetChannel(channelId);
            if (channel == null) {
                Console.WriteLine("Unable to access to channel.");
                return;
            }
            Console.WriteLine("Backing up channel: #"+ channel.Name);
            MessageContext dbContext = new MessageContext();
            IEnumerable<IMessage> messages = await channel.GetMessagesAsync(100).Flatten();
            IEnumerator<IMessage> messagesEnumerator = messages.GetEnumerator();
            bool finished = false;
            IMessage beforeLast = null;
            writeMessages: 
            messagesEnumerator.MoveNext();
            do {
                if (messagesEnumerator.Current == null) {
                    finished = true;
                    break;
                }
                dbContext.Messages.Add(new Message(messagesEnumerator.Current));
                if (messagesEnumerator.Current.Attachments.Count != 0) {
                    var attachments = messagesEnumerator.Current.Attachments.GetEnumerator();
                    for (int i = 0; i < messagesEnumerator.Current.Attachments.Count; i++) {
                        attachments.MoveNext();
                        dbContext.Attachments.Add(new Attachment(attachments.Current, channelId));
                    }
                    attachments.Dispose();
                }
                beforeLast = messagesEnumerator.Current;
                messagesEnumerator.MoveNext();
            } while (messagesEnumerator.Current != null);
            if (!finished) {
                messagesEnumerator.Dispose();
                messages = await channel.GetMessagesAsync(beforeLast.Id, Direction.Before, 100, RequestOptions.Default).Flatten();
                messagesEnumerator = messages.GetEnumerator();
                goto writeMessages;
            }
            dbContext.SaveChanges();
            Console.WriteLine("Message backup finished.");
        }
        static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();
    }
}
