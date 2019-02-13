# Remind Me v2.0

This repository contains the source code of the second version of the application that we will deploy to our Kubernetes cluster. You can read more about this application and the steps that you need to follow to deploy this application in chapter **four** of the book. This is a simple .Net Core Angular application that saves reminders in a persistent storage volume through a stateful back-end Web API.

## Getting started

Copy the source code of the application to your system. You will find two workspace files in the root folder. Open the workspace `remind-me-web.code-workspace` in Visual Studio Code. This workspace contains the front end web application which is present in the **web** folder in the project directory. The Angular application is present in the **/web/ClientApp/** directory.

Open the second workspace `remind-me-api.code-workspace` in another instance of Visual Studio Code. This workspace contains the back-end REST API which is present in the **/api/remind-me-api** folder in the project directory. Additionally, the workspace also contains the API unit test project in the **/api/api-tests** folder.

### Prerequisites

This project uses the following tools and technologies.

1. VS Code IDE
2. Docker
3. ASP.net Core on .Net Core 2.1
4. Angular SPA on Angular 5
5. Test Frameworks: Protractor, and Jasmine on Karma test runner
6. .Net Core 2.1 Web API
7. xUnit tests for .Net Core

### Installation

Build individual projects from the CLI or VS Code. For the front end project in the **web** directory, the build task will restore all the nuget packages and the npm dependencies. For the back end API project in the **remind-me-api**, the build task will restore the nuget packages.

The following instructions assume that you are using the command line for all the operations. Depending upon which project you are building, change to the **web** or **api** directory before executing the following commands. The following command will build the project.

```
dotnet build
```

To run the projects, change to the **web** folder for the web application and **/api/remind-me-api** folder for the API project and execute the following command.

```
dotnet run
```

Running the project from VS Code will automatically launch the browser with the application. The web application can be accessed on https://localhost:5001, and the api application can be accessed on https://localhost:5002. These links will also be displayed on the command line if you are using the CLI to execute the applications. Since the applications use a user-generated certificate, you can ignore the certificate error displayed by the browser.

## Running the tests

Unit tests in this project are written using the Jasmine framework running on Karma. The end to end tests are written using the Protractor framework. The unit tests for the Web API application are written using the xUnit framework. To execute the web application tests, change directory to **/web/ClientApp/** directory. Similarly, to execute the Web API unit tests, change to the **/api/api-tests** directory.

### Running the unit tests

The following command will run the unit tests in the front end web application.

```
ng test
```

The following command will run the end to end tests for the front end web application.

```
ng e2e
```

The following command will execute the unit tests for the Web API project.

```
dotnet test
```

## Deployment

The application is deployed as an image to DockerHub. The **web** folder contains the DockerFile used for building the web application image. The **\api\remind-me-api** folder contains the DockerFile used for building the Web API application image. After changing to the **web** directory, execute the following command to build the image for the web application.

```
docker build -t remind-me-web .
```

Next, after changing to the **\api\remind-me-api** folder, execute the following command to generate an image for the Web API application.

```
docker build -t remind-me-api .
```

Execute the following command to start a container named `remind-me-api-c` using the image that you previously generated for the Web API application. You will need to map a directory available on your system and mount it as a volume on the container. Follow the steps mentioned in this link to see how you can do so on your system: https://docs.docker.com/storage/volumes/. The following command assumes that system directory **C:/data** can be mapped to a volume on docker.

```
docker run -d -e dbPath=/data/Reminder.db -v C:/data:/data -p 8081:80 --name remind-me-api-c remind-me-api
```

Execute the following command to start a container named `remind-me-web-c` using the image of the web application that you previously generated.

```
docker run -d -e apiurl=http://host.docker.internal:8081 -p 8082:80 --name remind-me-web-c remind-me-web
```

Note how we have injected environment variables into the containers to supply connections string and API Url to the applications.

From your browser, navigate to http://localhost:8082 to view the web application. You can also check out the Open API UI of the API application at http://localhost:8081. Tag your images using the following commands one after the other.

```
docker tag remind-me-web kubernetessuccinctly/remind-me-web:2.0.0
docker tag remind-me-api kubernetessuccinctly/remind-me-api:1.0.0
```

To keep the image tags consistent with the book, we will never update the tags of these images.

We will now push the images to Docker Hub. To authenticate, execute the command `docker login` with your credentials. Push the image to Docker Hub using the following commands one after the other.

```
docker push kubernetessuccinctly/remind-me-web:2.0.0
docker push kubernetessuccinctly/remind-me-api:1.0.0
```

## Contributing

The contributions to this repository are limited to improving the quality of the solution without modifying its behavior. This will help us keep the solution relevant to the content in the book. If you are interested in collaborating, please raise an issue in the repository so that we can connect with you.

## Authors

- **Rahul Rai** - Creator - [rahulrai-in](https://github.com/rahulrai-in)
- **Tarun Pabbi** - Creator - [tarunp](https://github.com/tarunp)
