# uploadData.py
# Lily Zhang
# 8/2/19
# Purpose: transfer EV3 sensor data to Unity through MQTT connection for AR readings
# Finish execution with Ctrl-C
# References:
# http://www.steves-internet-guide.com/send-json-data-mqtt-python/
# https://github.com/mqtt/mqtt.github.io/wiki/public_brokers
# http://blog.jorand.io/2017/08/02/MQTT-on-Unity/

from ev3dev.ev3 import *
import ev3dev.ev3 as ev3 # package for EV3 Commands
import sys
import linecache
import requests,json # packages for Thingworx POST & GET
from time import sleep
from timeit import Timer

####### MQTT connection ##################

import paho.mqtt.client as mqtt

# This is the Publisher
broker="iot.eclipse.org"
client = mqtt.Client()
client.connect(broker)


#########################################


# global variable for ultrasonic sensor
distance=0


#Define motor outputs
motor_left = ev3.LargeMotor('outB')
motor_right = ev3.LargeMotor('outC')

# Connect EV3 sensors
cl = ColorSensor()
gy = GyroSensor()
ts = TouchSensor()
us = ev3.UltrasonicSensor()

# define settings for sensors
cl.mode='COL-COLOR' # Color Sensor in COLOR mode
colors=('unknown','black','blue','green','yellow','red','white','brown')
gy.mode='GYRO-ANG' # Put the gyro sensor into ANGLE mode
units = gy.units #reports degrees
us.mode='US-DIST-CM' # Put the US sensor into distance mode
units = us.units # reports 'cm' even though the sensor measures 'mm'

# returns distance from Ultrasonic Sensor in cm
# Note: 255.0 reading is filtered out
def getDist():
  global distance
  dist=us.value()/10 # convert mm to cm
  if dist==255.0:
    return distance
  distance=dist
  return dist  # convert mm to cm

# voltage level of EV3 battery
def getVoltage():
  with open('/sys/devices/platform/battery/power_supply/lego-ev3-battery/voltage_now','r') as f:
    data = f.read()
    # divide by a million for millivolts to volts
    # divide by 9 for fraction of total voltage
    voltage=(float(data)/9000000) 
    return voltage



# calls thingworxPOST for all sensors while program is running
def uploadData():
   while True:
      try:
        dataToUpload = {'distance':getDist(),'color':colors[cl.value()],'angle':(gy.value() % 360),'touch':ts.value(),'power':getVoltage()}
        payload=json.dumps(dataToUpload) # convert to a json string
        client.publish("topic/EV3ARProject", payload);
        print("payload published")
        sleep(1)
        #t = Timer(lambda: thingworxPOST(payload)) # for timing PUT request
        #print("Upload time:",t.timeit(number=1))
      except KeyboardInterrupt:
        client.disconnect()
        print("bye!")
        sys.exit(0)


if __name__=='__main__':
    uploadData()

