	    var fire = false;
	    var cdTime = 500;
	    var skillCD = 3000;

	    var firstFlash = true;
        var flashCount = 0;

        socket.emit("admin",socketID);

	    function sendMsg(e){
            	console.log(e);
				socket.emit(socketID, e); ///// emit(socketID , message)

                if(e =='u'){
                    console.log("helllllllloooooooooo");
                    document.getElementById("up").style.opacity=0.5;
                } else if(e =='d'){
                    document.getElementById("down").style.opacity=0.5;
                } else if(e =='l'){
                    document.getElementById("left").style.opacity=0.5;
                } else if(e =='r'){
                    document.getElementById("right").style.opacity=0.5;
                } else if(e =='s'){
                    document.getElementById("left").style.opacity=1;
                    document.getElementById("right").style.opacity=1;                    
                    document.getElementById("up").style.opacity=1;
                    document.getElementById("down").style.opacity=1;
                }			
            }	

			socket.on(socketID, function(msg){
				console.log(msg);
				if (msg == "gyroOff"){
					document.getElementById("phoneHolder").style.display = "none";
					document.getElementById("imgs").style.left = "30%";

                    document.getElementById("progress").value = 1;
					progressBarChange();
				}
				else if(msg == "gyroOn"){

						if(firstFlash){
                    		flashScreen();
                    		firstFlash = false;
                    	}
				 	    document.getElementById("phoneHolder").style.display = "block";
                        document.getElementById("imgs").style.left = "10%";
				}
			});
	
		function progressBarChange(){
			document.getElementById("progress").value += 1;
			console.log("a");
			if (document.getElementById("progress").value < 100){
 				setTimeout(function(){progressBarChange();}, 30);
			}
		}

		function flashScreen(){
            	if (flashCount%2 == 0){
            		document.body.style.backgroundColor = "black";
            	}
            	else{
            		document.body.style.backgroundColor = "white";
            	}
				
            	flashCount++;
            	if(flashCount < 16){
            		setTimeout(function(){
            		flashScreen();
            		},70);
            	}
            	
            }

        function DetectThrowingMovement(event){
	          var x = event.acceleration.x;
	          var y = event.acceleration.y;
	          var z = event.acceleration.z;

	          var acceTotal = Math.abs(x) + Math.abs(y) + Math.abs(z);

	          if ( !fire && acceTotal > 28){
	                fire = true;
	              		// console.log("fire!");
	               	    socket.emit(socketID, "fire");
	              setTimeout(function(){ fire = false; }, cdTime);
	          }
	      }

            function openNav() {
                document.getElementById("myNav").style.height = "100%";
            }

            function closeNav() {
                document.getElementById("myNav").style.height = "0%";
            }

            function getName(){
                var nickname=document.getElementById("nickname").value;
                sendMsg(nickname);
            }

            function findMe(){
            	socket.emit(socketID,'reset');
            }
      
		window.addEventListener('devicemotion', DetectThrowingMovement);