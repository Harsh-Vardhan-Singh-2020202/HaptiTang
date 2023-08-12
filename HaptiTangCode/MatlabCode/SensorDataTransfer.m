% Clearing command window and workspace
clc;
clear;

% Connect to phone and get sensor data
clear m
m = mobiledev;
m.AccelerationSensorEnabled = 1;
m.MagneticSensorEnabled = 1;
m.AngularVelocitySensorEnabled = 1;
m.Logging = 1;

% Initialize data for rolling plots
data = zeros(1, 9);

% Create a TCP/IP server
port = 55000;
server = tcpserver('192.168.21.128', port);

disp("Server started on port " + port);

% Wait for a client to connect
disp('Waiting for client connection...');
while ~server.Connected
    pause(1);
end
disp('Client connected.');

% Initialize EMA filters for magnetometer data
EMAFilterSize = 1; % Set the size of the EMA filter to 20 (last 20 points)
magEMAFilter = zeros(1, 3);
magneticBuffer = zeros(EMAFilterSize, 3);
bufferIndex = 1;

while true
    % Extract the accelerometer, magnetic field, and angular velocity data
    [accelData, ~] = accellog(m);
    [magneticData, ~] = magfieldlog(m);
    [angularVelData, ~] = angvellog(m);
   
    % Update the values with new data
    if size(accelData, 1) > 1
        data(1, 1:3) = accelData(end, :);
    end

    if size(magneticData, 1) > 1
        Update the magnetic buffer and calculate smoothed value
        magneticBuffer(bufferIndex, :) = magneticData(end, :);
        bufferIndex = bufferIndex + 1;
        if bufferIndex > EMAFilterSize
           bufferIndex = 1;
        end
        
        Calculate the smoothed magnetic value using the buffer
        magEMAFilter = sum(magneticBuffer) / EMAFilterSize;
        data(1, 4:6) = magEMAFilter;
        % data(1, 4:6) = magneticData(end, :);
    end
    
    if size(angularVelData, 1) > 1
        data(1, 7:9) = angularVelData(end, :);
    end

    % Send the combined data packet
    dataToSend = single(data(1, :));
    
    disp(dataToSend);
    
    fwrite(server, dataToSend, 'float32');
    
    % Wait for a short time before sending the next packet
    pause(0.02);

end 