$(document).ready(function () {
    /*Init Socket*/
    const socket = io();

  
    var container, stats;
    var camera, scene, renderer;
    var points;

    var mouseX = 0,
        mouseY = 0;

    var windowHalfX = window.innerWidth / 2;
    var windowHalfY = window.innerHeight / 2;

    init()
    animate()

    function init() {
        var container = document.getElementById('mainVisu');
        camera = new THREE.PerspectiveCamera(50, window.innerWidth / window.innerHeight, 1, 10000);


        camera.position.z = 300;
        scene = new THREE.Scene();

        light = new THREE.AmbientLight(0xffFF00);
        scene.add(light);



        renderer = new THREE.WebGLRenderer({
            canvas: container,
            antialias: true
        });
        renderer.setSize(window.innerWidth, window.innerHeight);

        document.body.appendChild(renderer.domElement);


    }

    /**
     * function animate() animates the scene
     */
    function animate() {
        requestAnimationFrame(animate);

        //render();
        renderer.render(scene, camera);
    }

    function drawPoint(data) {
        const vertices = [];

        for (let i = 0; i < (data.length-2); i++) {

            const x = data[i];
            const y = data[i+1];
            const z = data[i+2];

            vertices.push(x, y, z);

        }

        const geometry = new THREE.BufferGeometry();
        geometry.setAttribute('position', new THREE.Float32BufferAttribute(vertices, 3));

        const material = new THREE.PointsMaterial({
            color: 0x888888
        });

        points = new THREE.Points(geometry, material);

        scene.add(points);
    }

    function updatePoint(){
        
    }


    socket.on('data', function (json) {
        if (points==null){
            //drawPoint(json.data)
            console.log(json)
        }
    });



});