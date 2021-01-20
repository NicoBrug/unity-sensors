#!/usr/bin/env python3

import socket

HOST = '127.0.0.1'  # Standard loopback interface address (localhost)
PORT = 65432        # Port to listen on (non-privileged ports are > 1023)
backlog = 5 
size = 1024 

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen(5)

    conn, addr = s.accept()
    print ('Connected by', addr)
    while True:
            
            data = conn.recv(1024)
            print (data.decode('utf-8'))
        

print("Connexion interrompue.")
conn.close()