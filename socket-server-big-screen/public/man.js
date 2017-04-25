var cdTime=700;

    function sendMsg(e){
        console.log(socketID+" send: "+e);
        socket.emit(socketID,e);
    }
    socket.emit("admin",socketID);
    socket.emit(socketID,'reset');
    
    socket.on(socketID,function(msg){
        console.log(socketID+" on "+msg);
        if (msg == "gyroOff"){
            document.getElementById("phoneHolder").style.display = "none";
            document.getElementById("characterImg").style.left = "26%";

        }
        else if(msg == "gyroOn"){
            document.getElementById("phoneHolder").style.display = "block";
            document.getElementById("characterImg").style.left = "10%";
        }
    });

    sendMsg('reset');

    var gyroSwitch = false;
    var fire = false;
    var dataContainerMotion = document.getElementById('dataContainerMotion');


    function DetectThrowingMovement(){
        var x = event.acceleration.x;
        var y = event.acceleration.y;
        var z = event.acceleration.z;
        // var r = event.rotationRate;
        var acceTotal = Math.abs(x) + Math.abs(y) + Math.abs(z);

        if ( !fire && acceTotal > 28){
            fire = true;
            console.log("fire!");
            socket.emit(socketID, "fire");
            //document.getElementById('a').innerHTML += "fire<br>";
            setTimeout(function(){ fire = false; }, cdTime);
        }

        // dataContainerMotion.innerHTML = acceTotal;  

    }

    function send(msg){
        
        if(msg=='l'){
            document.getElementById("left").style.opacity=0.5;
        } else if(msg=='r'){
            document.getElementById("right").style.opacity=0.5;
        } else if(msg=='s'){
            document.getElementById("left").style.opacity=1;
            document.getElementById("right").style.opacity=1;
        }
        sendMsg(msg);
    }

    function findMe(){
        sendMsg('reset');
    }

    function openNav() {
        document.getElementById("myNav").style.height = "100%";

    }

    function closeNav() {
        document.getElementById("myNav").style.height = "0%";
    }

    document.ontouchmove = function(event){
        event.preventDefault();
    }


    window.addEventListener('devicemotion',DetectThrowingMovement);