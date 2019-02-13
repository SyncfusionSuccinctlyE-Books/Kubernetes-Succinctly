# Remind Me v3.0

This repository contains the source code of the third version of the application that we will deploy to our Kubernetes cluster. You can read more about this application and the steps that you need to follow to deploy this application in chapter **six** of the book. This version introduces an additional delete API to existing backend Web API. It also has a background job to process the reminders saved by the user of the web application.

## Getting started

Copy the source code of the application to your system. You will find two workspace files in the root folder. Open the workspace `remind-me-api.code-workspace` in one instance of Visual Studio Code. This workspace contains the back-end REST API which is present in the **/api/remind-me-api** folder in the project directory. Additionally, the workspace also contains the API unit test project in the **/api/api-tests** folder.

Open the second workspace `remind-me-job.code-workspace` in another instance of Visual Studio Code. This workspace contains the background job which is present in the **/jobs/remind-me-cron-job/** folder in the project directory.

### Prerequisites

This project uses the following tools and technologies.

1. VS Code IDE
2. Docker
3. ASP.net Core on .Net Core 2.1
4. .Net Core 2.1 Web API
5. xUnit tests for .Net Core

### Installation

Build individual projects from the CLI or VS Code. For the back end project in the **api** directory, the build task will restore all the nuget packages. For the job project in the **remind-me-cron-job** directory, the build task will restore the nuget packages.

The following instructions assume that you are using the command line for all the operations. Depending upon which project you are building, change to the **api** or **job** directory before executing the following commands. The following command will build the project.

```
dotnet build
```

To run the projects, change to the **api** folder for the API project and **/job/remind-me-cron-job** folder for the background job and execute the following command.

```
dotnet run
```

Running the project from VS Code will automatically launch the browser with the application. The api application can be accessed on https://localhost:5001. The background job will run as a console application. These links will also be displayed on the command line if you are using the CLI to execute the applications. Since the applications use a user-generated certificate, you can ignore the certificate error displayed by the browser.

## Running the tests

The unit tests for the Web API application are written using the xUnit framework. To execute the Web API unit tests, change to the **/api/api-tests** directory.

### Running the unit tests

The following command will execute the unit tests for the Web API project.

```
dotnet test
```

## Deployment

The application is deployed as an image to DockerHub. The **job/remind-me-cron-job** folder contains the DockerFile used for building the background job image. The **\api\remind-me-api** folder contains the DockerFile used for building the Web API application image. After changing to the **job/remind-me-cron-job** directory, execute the following command to build the image for the background job.

```
docker build -t remind-me-job .
```

Next, after changing to the **\api\remind-me-api** folder, execute the following command to generate an image for the Web API application.

```
docker build -t remind-me-api .
```

Execute the following command to start a container named `remind-me-api-c` using the image that you previously generated for the Web API application. You will need to map a directory available on your system and mount it as a volume on the container. Follow the steps mentioned in this link to see how you can do so on your system: https://docs.docker.com/storage/volumes/. The following command assumes that system directory **C:/data** is mapped to a volume on docker.

```
docker run -d -e dbPath=/data/Reminder.db -v C:/data:/data -p 8081:80 --name remind-me-api-c remind-me-api
```

Execute the following command to start a container named `remind-me-job-c` using the image of the job that you previously generated.

```
docker run -d -e apiurl=http://host.docker.internal:8081 -p 8082:80 --name remind-me-job-c remind-me-job
```

Note how we have injected environment variables into the containers to supply connections string and API Url to the applications.

You can check out the Open API UI of the API application at http://localhost:8081. Tag your images using the following commands one after the other.

```
docker tag remind-me-web kubernetessuccinctly/remind-me-job:1.0.0
docker tag remind-me-api kubernetessuccinctly/remind-me-api:2.0.0
```

To keep the image tags consistent with the book, we will never update the tags of these images.

We will now push the images to Docker Hub. To authenticate, execute the command `docker login` with your credentials. Push the image to Docker Hub using the following commands one after the other.

```
docker push kubernetessuccinctly/remind-me-job:1.0.0
docker push kubernetessuccinctly/remind-me-api:2.0.0
```

## Contributing

The contributions to this repository are limited to improving the quality of the solution without modifying its behavior. This will help us keep the solution relevant to the content in the book. If you are interested in collaborating, please raise an issue in the repository so that we can connect with you.

## Authors

- **Rahul Rai** - Creator - [rahulrai-in](https://github.com/rahulrai-in)
- **Tarun Pabbi** - Creator - [tarunp](https://github.com/tarunp)
