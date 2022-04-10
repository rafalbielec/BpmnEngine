# BpmnEngine

External BPMN processing engine based on Camunda diagrams.

## Inital set of features developed in BpmnEngine.Application

The intial scope signifies phase 1 of the project as a proof of concept how to support Camunda API and a BPMN diagram for a simple form request process.

- Support for External Service Tasks with variables
- Support for external process start-up procedure
- Support for message handling via the API to send Camunda events
- Support for Error Boundary Event returned by an External Service Task
- One HTML form in Razer to start a Camunda process via the API
- One HTML form in Razer used for request rejection and approval
- Email notification with a link to the web app for the point above
- Process completion notification via e-mail
- The web app will have a database layer for form tracing
- Optional support for user authentication against an identity server

![diagram.png](/wiki/diagram.png)
