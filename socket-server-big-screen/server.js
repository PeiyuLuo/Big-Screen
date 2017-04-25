
var express = require('express');
var app = express();

var http = require('http').Server(app);
var io = require('socket.io')(http);

// app.use('/', express.static('public'));
app.use(express.static('public'));

app.get('/', function(req, res){
   // console.log("REQ:: "+req.headers.host);
     // console.log('User-Agent: ' + req.headers['user-agent']);
	// console.log(__dirname);
  res.sendFile(__dirname + '/index.html');
});


var LeftID = 0;
var RightID = 0;
//man01, man02
var propsLeft = ['man01','man02','man05','b01', 'man10','b04','man11','b05','man22','b02', 
				 'man33','b06','man44','b03', 'man55','man66','man77','b21','man88','b22','man99',
				 'b20','man777','b23','man789','b24','man876','b25','man888','b26'];

var propsRight = ['man111','b29','man123','b30','man135','man222','b31','man234','b32','man246',
				  'b33','man321','b34','man333','man345','b35','man432','b36','man444','b37',
				  'man456','b38','man543','b39','man555','b40','man567','b41','man654','b42'];

var IDs = [];
var ID_props = [];

app.get('/b', function(req, res){
    // console.log('Balloon0'+balloonID);
    if(LeftID < propsLeft.length){
    	res.redirect('/' + propsLeft[LeftID] + '.html');
		LeftID++;
    }
    else{
// http://104.131.106.20:8888/c
      res.redirect('/errorB.html');
    }
});



app.get('/c', function(req, res){
	if(RightID < propsRight.length){
		res.redirect('/' + propsRight[RightID] + '.html');
   	    RightID++;
	}
	else{
    res.redirect('/erorC.html');
	}
    
});



io.on('connection', function(socket){
	console.log("We have a new client: " + socket.id);


  // console.log(socket);

  	socket.on('a', function(msg){
  		// console.log(msg);
   	    io.emit('a', msg);
   	    // socket.broadcast.send(msg);
 	 });

  	socket.on('adminLeft', function(msg){
  		propsLeft[propsLeft.length] = msg;
      console.log(propsLeft);
 	 });
  	socket.on('adminRight', function(msg){
  		propsRight[propsRight.length] = msg;
 	 });

    socket.on('admin', function(msg){
       IDs.push(socket.id);
       ID_props.push(msg);
       io.emit('userLogIn', msg);
       console.log(IDs);
     });
    socket.on('disconnect', function() {
      for (i=0; i<IDs.length; i++){
        if(IDs[i] == socket.id){
          io.emit('userLogOut', ID_props[i]);
        }
      }
    });

    socket.on('reset', function(msg){
      console.log('receive reset message');
      RightID = 0;
      LeftID = 0;

      IDs = [];
      ID_props = [];

      var propsLeft = ['man01','man02','man05','b01', 'man10','b04', 'FruitTruck09','man11','b05','man22',
                       'b02','man33','b06','man44','b03', 'man55','man66','man77','b21','man88',
                       'b22','man99','b20','man777','b23','man789','b24','man876','b25','man888','b26',
                       'man987','b27','man999','b28','man111','man123','man135','man900','man901','man902',
                       'man903','man904','man905','man906','man907','man908','man909','man910','man911',
                       'man912','man913','man914','man915'];

      var propsRight = ['man222','b31','man234','b32','man246',
                        'b33','man321','b34','man333','man345','b35','man432','b36','man444','b37',
                        'man456','b38','man543','b39','man555','b40','man567','b41','man654','b42',
                        'man666','b43','man678','man765','b30','b29','man800','man801','man802',
                        'man803','man804','man805','man806','man807','man808','man809','man810',
                        'man811','man812','man813','man814','man815','man816','man817','man818','man819','man820',
                        'man821','man822','man823','man824','man825'];
  	});


    socket.on('leftInfo', function(msg){ io.emit('leftInfo', msg);});
    socket.on('rightInfo', function(msg){ io.emit('rightInfo', msg); });
    socket.on('top1', function(msg){ io.emit('top1', msg);});
    socket.on('top2', function(msg){ io.emit('top2', msg); });
    socket.on('top3', function(msg){ io.emit('top3', msg);});
    socket.on('top4', function(msg){ io.emit('top4', msg); });

  	socket.on('b01', function(msg){ io.emit('b01', msg);});
  	socket.on('b02', function(msg){ io.emit('b02', msg); });
  	socket.on('b03', function(msg){ io.emit('b03', msg);});
  	socket.on('b04', function(msg){ io.emit('b04', msg); });
  	socket.on('b05', function(msg){ io.emit('b05', msg);});
  	socket.on('b06', function(msg){ io.emit('b06', msg); });

    socket.on('b20', function(msg){ io.emit('b20', msg);});
	socket.on('b21', function(msg){io.emit('b21', msg); });
	socket.on('b22', function(msg){io.emit('b22', msg); });
	socket.on('b23', function(msg){io.emit('b23', msg); });
	socket.on('b24', function(msg){io.emit('b24', msg); });
	socket.on('b25', function(msg){io.emit('b25', msg); });
	socket.on('b26', function(msg){io.emit('b26', msg); });
	socket.on('b27', function(msg){io.emit('b27', msg); });
	socket.on('b28', function(msg){io.emit('b28', msg); });
	socket.on('b29', function(msg){io.emit('b29', msg); });
	socket.on('b30', function(msg){io.emit('b30', msg); });
	socket.on('b31', function(msg){io.emit('b31', msg); });
	socket.on('b32', function(msg){io.emit('b32', msg); });
	socket.on('b33', function(msg){io.emit('b33', msg); });
	socket.on('b34', function(msg){io.emit('b34', msg); });
	socket.on('b35', function(msg){io.emit('b35', msg); });
	socket.on('b36', function(msg){io.emit('b36', msg); });
	socket.on('b37', function(msg){io.emit('b37', msg); });
	socket.on('b38', function(msg){io.emit('b38', msg); });
	socket.on('b39', function(msg){io.emit('b39', msg); });
	socket.on('b40', function(msg){io.emit('b40', msg); });
	socket.on('b41', function(msg){io.emit('b41', msg); });
	socket.on('b42', function(msg){io.emit('b42', msg); });
	socket.on('b43', function(msg){io.emit('b43', msg); });

  	//CHARACTERS
	 socket.on('man01', function(msg){ io.emit('man01', msg); });
 	 socket.on('man02', function(msg){ io.emit('man02', msg);});
 	 socket.on('man03', function(msg){ io.emit('man03', msg);});
 	 socket.on('man04', function(msg){ io.emit('man04', msg);});
     socket.on('man05', function(msg){ io.emit('man05', msg);});
	 socket.on('man10', function(msg){ io.emit('man10', msg);});
	 socket.on('man11', function(msg){ io.emit('man11', msg);});
     socket.on('man31', function(msg){ io.emit('man31', msg);});
     socket.on('man22', function(msg){ io.emit('man22', msg);});
     socket.on('man33', function(msg){ io.emit('man33', msg);});
     socket.on('man44', function(msg){ io.emit('man44', msg);});
     socket.on('man55', function(msg){ io.emit('man55', msg);});
     socket.on('man66', function(msg){ io.emit('man66', msg); });
     socket.on('man77', function(msg){ io.emit('man77', msg); });
     socket.on('man88', function(msg){io.emit('man88', msg); });
     socket.on('man99', function(msg){io.emit('man99', msg);});
     socket.on('man111', function(msg){io.emit('man111', msg); });
     socket.on('man123', function(msg){io.emit('man123', msg); });
     socket.on('man135', function(msg){io.emit('man135', msg); });
     socket.on('man222', function(msg){io.emit('man222', msg); });
     socket.on('man234', function(msg){io.emit('man234', msg); });
     socket.on('man246', function(msg){io.emit('man246', msg); });
     socket.on('man321', function(msg){io.emit('man321', msg); });
     socket.on('man333', function(msg){io.emit('man333', msg); });
     socket.on('man345', function(msg){io.emit('man345', msg); });
     socket.on('man432', function(msg){io.emit('man432', msg); });
     socket.on('man444', function(msg){io.emit('man444', msg); });
     socket.on('man456', function(msg){io.emit('man456', msg); });
     socket.on('man543', function(msg){io.emit('man543', msg); });
     socket.on('man555', function(msg){io.emit('man555', msg); });
     socket.on('man567', function(msg){io.emit('man567', msg); });
     socket.on('man654', function(msg){io.emit('man654', msg); });
     socket.on('man666', function(msg){io.emit('man666', msg); });
     socket.on('man678', function(msg){io.emit('man678', msg); });
     socket.on('man765', function(msg){io.emit('man765', msg); });
     socket.on('man777', function(msg){io.emit('man777', msg); });
     socket.on('man789', function(msg){io.emit('man789', msg); });
     socket.on('man876', function(msg){io.emit('man876', msg); });
     socket.on('man888', function(msg){io.emit('man888', msg); });
     socket.on('man987', function(msg){io.emit('man987', msg); });
     socket.on('man999', function(msg){io.emit('man999', msg); });

     socket.on('man900', function(msg){io.emit('man900', msg); });
     socket.on('man901', function(msg){io.emit('man901', msg); });
     socket.on('man902', function(msg){io.emit('man902', msg); });
     socket.on('man903', function(msg){io.emit('man903', msg); });
     socket.on('man904', function(msg){io.emit('man904', msg); });
     socket.on('man905', function(msg){io.emit('man905', msg); });
     socket.on('man906', function(msg){io.emit('man906', msg); });
     socket.on('man907', function(msg){io.emit('man907', msg); });
     socket.on('man908', function(msg){io.emit('man908', msg); });
     socket.on('man909', function(msg){io.emit('man909', msg); });
     socket.on('man910', function(msg){io.emit('man910', msg); });
     socket.on('man911', function(msg){io.emit('man911', msg); });
     socket.on('man912', function(msg){io.emit('man912', msg); });
     socket.on('man913', function(msg){io.emit('man913', msg); });
     socket.on('man914', function(msg){io.emit('man914', msg); });
     socket.on('man915', function(msg){io.emit('man915', msg); });
     socket.on('man916', function(msg){io.emit('man916', msg); });
     socket.on('man917', function(msg){io.emit('man917', msg); });
     socket.on('man918', function(msg){io.emit('man918', msg); });
     socket.on('man919', function(msg){io.emit('man919', msg); });

     socket.on('man800', function(msg){io.emit('man800', msg); });
     socket.on('man801', function(msg){io.emit('man801', msg); });
     socket.on('man802', function(msg){io.emit('man802', msg); });
     socket.on('man803', function(msg){io.emit('man803', msg); });
     socket.on('man804', function(msg){io.emit('man804', msg); });
     socket.on('man805', function(msg){io.emit('man805', msg); });
     socket.on('man806', function(msg){io.emit('man806', msg); });
     socket.on('man807', function(msg){io.emit('man807', msg); });
     socket.on('man808', function(msg){io.emit('man808', msg); });
     socket.on('man809', function(msg){io.emit('man809', msg); });
     socket.on('man810', function(msg){io.emit('man810', msg); });
     socket.on('man811', function(msg){io.emit('man811', msg); });
     socket.on('man812', function(msg){io.emit('man812', msg); });
     socket.on('man813', function(msg){io.emit('man813', msg); });
     socket.on('man814', function(msg){io.emit('man814', msg); });
     socket.on('man815', function(msg){io.emit('man815', msg); });
     socket.on('man816', function(msg){io.emit('man816', msg); });
     socket.on('man817', function(msg){io.emit('man817', msg); });
     socket.on('man818', function(msg){io.emit('man818', msg); });
     socket.on('man819', function(msg){io.emit('man819', msg); });
     socket.on('man820', function(msg){io.emit('man820', msg); });
     socket.on('man821', function(msg){io.emit('man821', msg); });
     socket.on('man822', function(msg){io.emit('man822', msg); });
     socket.on('man823', function(msg){io.emit('man823', msg); });
     socket.on('man824', function(msg){io.emit('man824', msg); });
     socket.on('man825', function(msg){io.emit('man825', msg); });
     socket.on('man826', function(msg){io.emit('man826', msg); });
     socket.on('man827', function(msg){io.emit('man827', msg); });
     socket.on('man828', function(msg){io.emit('man828', msg); });
     socket.on('man829', function(msg){io.emit('man829', msg); });


    ///VANS
  socket.on('iceCreamVan08', function(msg){ io.emit('iceCreamVan08', msg); });
  socket.on('iceCreamVan1008', function(msg){ io.emit('iceCreamVan1008', msg);});

  socket.on('FruitTruck09', function(msg){ io.emit('FruitTruck09', msg);});
  socket.on('FruitTruck2016', function(msg){ io.emit('FruitTruck2016', msg);});

});

http.listen(8888, function(){
  console.log('listening on port:8888');
});