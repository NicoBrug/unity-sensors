# unity-sensors

## About The Project

This project aims to build tools for numerical simulation with unity in particular sensors.
Parameterizable components will be available (Generic component) as well as tools that can be quickly integrated according to different manufacturers' specifications.These components are intended for use with robotic development platforms such as ROS(2), as well as for testing AI or SLAM algorithms in python or other languages. In order to simplify the export of sensor data from the simulation, it is possible to use the dedicated python server to retrieve the data and then test the developed systems.The data is serialized in JSON then sent in UDP from Unity to the python server. Afterwards, the server generates a json file that will be updated with the new data.


## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

* python 3.9 
* unity
* nodejs
* npm

### Installation
1. Clone the repo in unity project
   ```sh
   git clone https://github.com//NicoBrug/unity-sensors
   ```
2. Add the scripts to your unity GameObjects
2. run python udp server
   ```sh
   py udp_server.py
   ```
3. run app
   ```sh
   node app.js
   ```
4. Run unity simulation
5. Get data from sensor and start to build things !
5. Open brower to "localhost:3000" and see 3D POC!

## Roadmap

### sensors
- [x] Lidar3D (poc)
- [x] Lidar2D (poc)
- [x] Distance 
- [ ] Depth Camera
- [ ] Camera
- [ ] Constructor specs (RPLidar, velodyne, ouster,intel) 



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.

<!-- CONTACT -->
## Contact
Nicolas Brugie - nicolasbrugie@gmail.com
