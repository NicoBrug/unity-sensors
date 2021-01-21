#!/usr/bin/env python3

import socket
import json 
import http.server
import socketserver
import http.server
import socketserver

class Server:
    def __init__(self):
        self.HOST = '127.0.0.1'
        self.PORT = 12345
        self.size = 1024
        self.data = []
        self.rcv = True

    def connect(self):
        with socket.socket(socket.AF_INET, socket.SOCK_DGRAM) as s:
            s.bind((self.HOST, self.PORT))
            self.startReceive(s)
            

    def startReceive(self,s):
        while self.rcv:
            data,addr = s.recvfrom(self.size)
            data.decode('utf-8')
            jsondata = json.loads(data)
            print(jsondata["type"])

            if not data:
                break

    def stopReceive(self):
        self.rcv = False


    
server = Server()
server.connect()
