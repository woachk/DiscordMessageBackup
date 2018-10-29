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
    public class Message {
         public bool IsTTS { get; set; }
         public bool IsPinned { get; set;}
         public string Content { get; set;}
         public string UserName {get; set;}
         public string ChannelName {get; set;}
         public ulong ChannelId {get; set;}
         public ulong UserId {get; set;}
         [Key] public ulong Id {get; set;}
         public Message() {

         }
         public Message(IMessage msg) {
             Id = msg.Id;
             IsTTS = msg.IsTTS;
             IsPinned = msg.IsPinned;
             Content = msg.Content;
             UserName = msg.Author.Username;
             UserId = msg.Author.Id;
             ChannelName = msg.Channel.Name;
             ChannelId = msg.Channel.Id;
         }
         
    }
    public class Attachment {
        public ulong ChannelId {get; set;}
        [Key] public ulong Id {get; set;}
        public string Url {get; set;}
        public string ProxyUrl {get; set;}
        public string FileName {get; set;}
        public uint Width {get; set;}
        public uint Height {get; set;}
        public Attachment (IAttachment attachment, ulong channel) {
            ChannelId = channel;
            Id = attachment.Id;
            Url = attachment.Url;
            ProxyUrl = attachment.ProxyUrl;
            FileName = attachment.Filename;
            if (attachment.Width != null && attachment.Height != null ) {
                // width and height cannot be negative values.
                Width = (uint)attachment.Width.Value;
                Height = (uint)attachment.Height.Value;
            }
        }
        public Attachment() {

        }
    }
    public class MessageContext : DbContext {
        public DbSet<Message> Messages {get; set;}
        public DbSet<Attachment> Attachments {get; set;}
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=test.db");
        }
    }
}