# BpmnEngine

External BPMN processing engine based on Camunda diagrams.

## Features developed in BpmnEngine.Application

This project is a proof of concept on how to support Camunda API and BPMN diagrams with external tasks.

- Support for External Service Tasks with variables
- Support for external process start-up procedures
- Support for message handling via the API to send events
- Two HTML forms in Razer to start a Camunda process via the API
- One HTML form in Razer used for request rejection and approval
- Email notifications with a link to the web app to make decisions
- Process completion notification via e-mail
- The web app will have a database layer for form tracing
- The web app will have user logins stored in the local database
- Support for user authentication to distinguish between different job positions

![diagram.png](/wiki/diagram.png)
