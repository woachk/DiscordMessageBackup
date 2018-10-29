#!/bin/sh

sqlite3 ../test.db < script.sql > files.txt
wget -i files.txt
