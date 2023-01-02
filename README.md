# Stripe Connector

Microservice used for mediating between a Web API and the Stripe platform. Provides generic functionality that can be scaled up depending on the system requirements.

To run locally, use the command: 
###### dotnet run

To run inside Docker, run the commands:
###### docker build -t stripe_web_api .
###### docker-compose up

To see a list of available endpoints, access the Swagger doc at http://localhost:5200/swagger/index.html. Any URL in the Doc has to be appended to the service's address (http://localhost:5200). The Production URL (inside the Docker container) is http://localhost:5000.

Before running it, replace the [place key here] placeholder under the project root's "appsettings.json" with the secret key from your Stripe account.
