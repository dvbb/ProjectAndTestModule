## docker tutorial

#### login
* docker login -u "user" -p "password" docker.io

#### create images
* docker build .\samplefolder -t imagename

quick website example: 
```
& git clone https://github.com/Azure-Samples/aci-helloworld.git
```

#### push a image to hub
previous step will create a image in local
* docker tag imagename username/iamgename:1.0.0
* docker push username/iamgename:1.0.0

Then you can find this image in [docker](https://hub.docker.com/repositories/), and feel free to use it in any cloud provider services
```
docker pull <user>/<iamgename>:1.0.0
```

#### run a image(create a container)
* docker run -d -p 8080:80 imagename

Then can browse http://localhost:8080/ to check your website
>NOTE: This [link](https://docs.docker.com/engine/reference/commandline/run/#options) can look up all command parameters  
`-d		Run container in background and print container ID`  
`-p		Publish a container's port(s) to the host`  

