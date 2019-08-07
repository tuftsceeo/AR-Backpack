# AR-Backpack
Augmented reality app to display sensor data
Generalized Augmented Reality App for Displaying Sensor and Robotic Data
Tufts University Center for Engineering Education and Outreach
Summer 2019
Main Contributors: Lily Zhang

This Unity project is an augmented reality application that uses MQTT communication to retrieve all information sent to a broker, and displays it in a translucent text bubble over an image target. This project can be adapted for many purposes, such as visualizing sensor data, or reporting the planned path of a moving robot.

ARBackpack: Unity project containing AR app. textEdit.cs, found under Assets, contains the subscriber and subscribed topic, and more information about how to modify the communication method.

mqttPublisher.py: publishes EV3 sensor data to a public broker. This is an example of how the app can visualize useful data.

thingmark.docx: image target for AR app.
