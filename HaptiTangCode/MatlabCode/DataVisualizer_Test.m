% Clearing commond window and workspace
clc;

% Connect to phone and get sensor data
clear m
m = mobiledev;
m.AccelerationSensorEnabled = 1;
m.MagneticSensorEnabled = 1;
m.AngularVelocitySensorEnabled = 1;
m.Logging = 1;

% Initialize data for rolling plots
numDataPoints = 200;
dataAccel = zeros(numDataPoints, 3);
dataMagnetic = zeros(numDataPoints, 3);
dataAngularVel = zeros(numDataPoints, 3);

% Initialize plots for accelerometer, magnetic field, and angular velocity
figure('Name', 'Sensor Data', 'NumberTitle', 'off')

% Accelerometer subplot
subplot(3, 1, 1);
hold on;
lineAccelX = plot(dataAccel(:, 1), 'r', 'LineWidth', 2);
lineAccelY = plot(dataAccel(:, 2), 'g', 'LineWidth', 2);
lineAccelZ = plot(dataAccel(:, 3), 'b', 'LineWidth', 2);
hold off;
ylabel('Acceleration');
title('Accelerometer');
legend('X', 'Y', 'Z');

% Magnetic field subplot
subplot(3, 1, 2);
hold on;
lineMagneticX = plot(dataMagnetic(:, 1), 'r', 'LineWidth', 2);
lineMagneticY = plot(dataMagnetic(:, 2), 'g', 'LineWidth', 2);
lineMagneticZ = plot(dataMagnetic(:, 3), 'b', 'LineWidth', 2);
hold off;
ylabel('Magnetic Field');
title('Magnetometer');
legend('X', 'Y', 'Z');

% Angular velocity subplot
subplot(3, 1, 3);
hold on;
lineAngularVelX = plot(dataAngularVel(:, 1), 'r', 'LineWidth', 2);
lineAngularVelY = plot(dataAngularVel(:, 2), 'g', 'LineWidth', 2);
lineAngularVelZ = plot(dataAngularVel(:, 3), 'b', 'LineWidth', 2);
hold off;
xlabel('Time Steps');
ylabel('Angular Velocity');
title('Gyroscope');
legend('X', 'Y', 'Z');

axis([0 numDataPoints -25 25]);

pause(1)
working = true;
while working

    % Extract the accelerometer, magnetic field, and angular velocity data
    [accelData, ~] = accellog(m);
    [magneticData, ~] = magfieldlog(m);
    [angularVelData, ~] = angvellog(m);

    % Update the accelerometer plot with new data
    if size(accelData, 1) > numDataPoints
        dataAccel = accelData(end-numDataPoints+1:end, :);
    else
        dataAccel(1:size(accelData, 1), :) = accelData;
    end
    lineAccelX.YData = dataAccel(:, 1);
    lineAccelY.YData = dataAccel(:, 2);
    lineAccelZ.YData = dataAccel(:, 3);

    % Update the magnetic field plot with new data
    if size(magneticData, 1) > numDataPoints
        dataMagnetic = magneticData(end-numDataPoints+1:end, :);
    else
        dataMagnetic(1:size(magneticData, 1), :) = magneticData;
    end
    lineMagneticX.YData = dataMagnetic(:, 1);
    lineMagneticY.YData = dataMagnetic(:, 2);
    lineMagneticZ.YData = dataMagnetic(:, 3);

    % Update the angular velocity plot with new data
    if size(angularVelData, 1) > numDataPoints
        dataAngularVel = angularVelData(end-numDataPoints+1:end, :);
    else
        dataAngularVel(1:size(angularVelData, 1), :) = angularVelData;
    end
    lineAngularVelX.YData = dataAngularVel(:, 1);
    lineAngularVelY.YData = dataAngularVel(:, 2);
    lineAngularVelZ.YData = dataAngularVel(:, 3);
    
    drawnow
    
    % Check for a key press without blocking
    fig = gcf;
    set(fig, 'KeyPressFcn', @(src, event) keyPressCallback(event));
end

function keyPressCallback(event)
    if strcmp(event.Key, 'escape')
        fprintf('Stopping Visualization\n');
        evalin('base', 'working = false;');
    end
end