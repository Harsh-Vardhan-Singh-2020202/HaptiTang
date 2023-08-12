% Clearing commond window and workspace
clc;

% Create a TCP/IP server
port = 55000; % Replace with your desired port number
server = tcpserver('0.0.0.0', port);

disp("Server started on port " + port);

% Wait for a client to connect
disp('Waiting for client connection...');
while ~server.Connected
    pause(1);
end
disp('Client connected.');

% Send the integer value 23
a = int32(23);
fwrite(server, typecast(a, 'uint8'), 'uint8');

% Close the server when done
fclose(server);
