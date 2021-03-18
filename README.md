# unity-sensors

## ℹ️️ Description

This project aims to build tools for numerical simulation with unity in particular sensors.
Parameterizable components will be available (Generic component) as well as tools that can be quickly integrated according to different manufacturers' specifications.These components are intended for use with robotic development platforms such as ROS(2), as well as for testing AI or SLAM algorithms in python or other languages. In order to simplify the export of sensor data from the simulation, it is possible to use the dedicated python server to retrieve the data and then test the developed systems.The data is serialized in JSON then sent in UDP from Unity to the python server. 

![Screenshot](/pics/lidar.PNG)

## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

* python 3.9 
* unity


## 🔧 How to Use
1. Clone the repo in unity project
   ```sh
   git clone https://github.com//NicoBrug/unity-sensors
   ```
2. Add the scripts to your unity GameObjects
3. Create material, Add Shader point
4. Run unity simulation
5. Get data from sensor and start to build things !


## Roadmap

### sensors
- Lidar3D (poc)
   - [x] Ouster OS0
   - [x] Ouster OS1
   - [x] Ouster OS2
   - [x] Velodyne Alpha Prime
   - [x] Velodyne HDL32
   - [x] Velodyne Puck
   - [x] Velodyne Ultra Puck   
- [x] Lidar2D (poc)
- [x] Distance 
- [ ] Depth Camera
- [ ] Camera




<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.

<!-- CONTACT -->
## Contact
Nicolas Brugie - nicolasbrugie@gmail.com
