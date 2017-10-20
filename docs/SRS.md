# Unveiled - Software Requirements Specification

## 1. Introduction
### 1.1 Purpose
This SRS describes all specifications for "NeCo". It’s an Xamarin-App. "NeCo" allows
users to chat with people in their near environment. In this document the usage of the
"NeCo"-Xamarin-App will be explained. Furthermore reliability, reaction speed and other important
characteristics of this project will be specified. This includes design and architectural decisions regarding optimization of
these criteria as well.

### 1.2 Scope
This software specification applies to the whole "NeCo" application. The app allows users to chat with strangers in their near environment to meet new people, which are at the same spot as the user. It establishes user- and systemcreated chatrooms amon users in close environments. 


### 1.3 Definitions, Acronyms and Abbreviations
In this section definitions and explanations of acronyms and abbreviations are listed to help the reader to understand these.

- **Android** This is a mobile operating system developed by Google for primarily use on smartphones and tablets.
- **UC** Use Case
- **UCD** Use Case Diagram
- **OUCD** Overall Use Case Diagram
- **SAD** Software Architecture Document
- **RFC** Request for Comments


### 1.4 References
|			Title									|	Date		|
|---------------------------------------------------|---------------|
| [NeCo Blog](http:/necoproject.wordpress.com/) | 19.10.2017 |
| [Ouverall Use Case Diagramm (OUCD)](https://github.com/Haus4/NeCo/raw/master/docs/UseCaseDiagramm.jpg)| 19.10.2017 |

### 1.5 Overview
The following chapters are about our vision and perspective, the software requirements, the demands we have, licensing and
the technical realization of this project.

## 2. Overall Description
### 2.1 Vision
Our idea is to develop a Xamarin App for chatting with strangers around you.
The basic idea behind NeCo is to use localization technologies to create chatrooms with people around you, similar to the popular app “Jodel”.  The difference is that you chat with random people in your local area, to meet, to party, or just to have fun.
For security reasons we’ll use latest encrypting technology like RSA&AES-Encrypting. Ideas for future features could be a friend list to keep in contact.

People using our App can chat with strangers in their near environment. 

The following picture shows the overall use case diagram of our software:
![Ouverall Use Case Diagramm (OUCD)](https://github.com/Haus4/NeCo/raw/master/docs/UseCaseDiagramm.jpg "Ouverall Use Case Diagramm (OUCD)")


## 3. Specific Requirements
### 3.1 Functionality - Android App
#### 3.1.1 Chat
The app provides the user with the possibility to chat with another person.

#### 3.1.2 Share files
The user is able to share files with the chatroom.

#### 3.1.3 Chat with people nearby
The app enables the user to chat with people in his near environment. This is provided through localization within the functionality of the smartphone.

#### 3.1.4 Chat encrypted
The app provides an encypted chat, where messages get encrypted with RSA&AES-Encrypting.

#### 3.1.5 Moderated Chat
The app provides naming a moderator, who is able to kick or ban users, delete messages.

#### 3.1.6 Manage Profile
The user is able to change his/her nickname and profile picture within the app. Also managing a friendlist by adding and deleting friends is possible.

#### 3.1.7 Pointsystem
The app provides a reward system where the user gets points for joining an chatroom or using the app.


### 3.2 Usability
#### 3.2.1 Smartphone user
The user should know how to use Android as an mobile operating system and how to install and use an mobile application on it. We will provide a installation guide.

### 3.3 Reliability
#### 3.3.1 Server availability
Our own server should ensure a 95% up-time.

Our server is co-hosted at the DHBW so we must rely on their service.

### 3.4 Performance
The sending of the messages and files files from one User to another must not guarantee real-time data transfer, because the message and files will not be displayed and watched live. Nevertheless the transfer should not take longer than 5 seconds to ensure fast respond times.

### 3.5 Supportability
#### 3.5.1 Language support
We will use the following languages, which will also be well supported in the future:

- C#
- Android

### 3.6 Design Constraints
All information about the architectural design of our application stack can be found in our [software architecture document][sad]. In the following subchapters you can read about some generall important decisions.

#### 3.6.1 MVC architecture
Our Android application should implement the MVC pattern.

### 3.7 On-line User Documentation and Help System Requirements
The whole application will be built with an intuitive design, so there shouldn’t be a need for the user to ask us or the program for help. However we will write our own blog, on which users can find information and ask us questions.

### 3.8 Purchased Components
(n/a)

### 3.9 Interfaces
#### 3.9.1 User Interfaces
Please consult the different use case descriptions for UI mockups (screenshots) and UI functionality descriptions:

- [UC1: Chat][uc capture video]
- [UC3: Local Chat][uc maintain profile]
- [UC2: Share files][uc configure settings]
- [UC4: Encrypted chat][uc switch user]
- [UC5: Manage profile][uc register]
- [UC6: Manage friends in friendlist][uc browse media]
- [UC7: Get Points][uc manage users]
- [UC8: Manage chatrooms][uc delete own media]
- [UC9: Name moderator][uc download own media]
- [UC10: Kick/Ban user][uc view own media]
- [UC11: Delete message][uc approve registration]

#### 3.10.2 Hardware Interfaces
(n/a)

#### 3.10.3 Software Interfaces
(n/a)

#### 3.10.4 Communications Interfaces
(n/a)

### 3.11 Licensing Requirement


### 3.12 Legal, Copyright and other Notices
(n/a)

### 3.13 Applicable Standards


## 4. Supporting Information
### 4.1 Appendices
You can find any internal linked sources in the chapter References (go to the top of this document). If you would like to know what the current status of this project is please visit the [NeCo Blog][blog].



[Overall Use Case Diagram (OUCD)]: https://github.com/Haus4/NeCo/blob/master/docs/UseCaseDiagramm.jpg "Link to Github"

[uc capture video]: http://unveiled.systemgrid.de/wp/docu/srs_uc1/ "Use Case 1: Capture and stream video"
[uc configure settings]: http://unveiled.systemgrid.de/wp/docu/srs_uc2/ "Use Case 2: Configure settings"
[uc maintain profile]: http://unveiled.systemgrid.de/wp/docu/srs_uc3/ "Use Case 3: Maintain profile"
[uc switch user]: http://unveiled.systemgrid.de/wp/docu/srs_uc4/ "Use Case 4: Switch user"
[uc register]: http://unveiled.systemgrid.de/wp/docu/srs_uc5/ "Use Case 5: Register"
[uc browse media]: http://unveiled.systemgrid.de/wp/docu/srs_uc6/ "Use Case 6: Browse own media"
[uc manage users]: http://unveiled.systemgrid.de/wp/docu/srs_uc7/ "Use Case 7: Manage users"
[uc delete own media]: http://unveiled.systemgrid.de/wp/docu/srs_uc8/ "Use Case 8: Delete own media"
[uc download own media]: http://unveiled.systemgrid.de/wp/docu/srs_uc9/ "Use Case 9: Download own media"
[uc view own media]: http://unveiled.systemgrid.de/wp/docu/srs_uc10/ "Use Case 10: View own media"
[uc approve registration]: http://unveiled.systemgrid.de/wp/docu/srs_uc11/ "Use Case 11: Approve registration"
[uc upload file]: http://unveiled.systemgrid.de/wp/docu/srs_uc12/ "Use Case 12: Upload file"

[sad]: http://unveiled.systemgrid.de/wp/docu/sad/ "Software Architecture Document"
[testplan]: http://unveiled.systemgrid.de/wp/docu/testplan/ "Testplan"
[blog]: http://unveiled.systemgrid.de/wp/blog/ "Unveiled Blog"
[website]: http://unveiled.systemgrid.de/ "Unveiled Website"
[jira]: http://jira.it.dh-karlsruhe.de:8080/secure/RapidBoard.jspa?rapidView=10&projectKey=UNV "Jira Unveiled Scrum Board"
[github]: https://github.com/SAS-Systems "Sourcecode hosted at Github"
[presentation]: https://github.com/SAS-Systems/Unveiled-Documentation/blob/master/Unveiled_Presentation_Final.pptx "Final project presentation"
[installation guide]: http://unveiled.systemgrid.de/wp/docu/installation/ "Android App Installation Guide"
[fpc]: http://unveiled.systemgrid.de/wp/docu/fpc/ "Function point calculation and use case estimation"

[RFC 3550]: https://tools.ietf.org/html/rfc3550
[RFC 2326]: https://tools.ietf.org/html/rfc2326
[RFC 1889]: https://www.ietf.org/rfc/rfc1889.txt

[libstreaming]: https://github.com/fyhertz/libstreaming

<!-- Picture-Link definitions: -->
[OUCD]: https://raw.githubusercontent.com/SAS-Systems/Unveiled-Documentation/master/Bilder/UC_Diagrams/Unveiled_Overall%20Use%20Case%20Diagram.png "Overall Use Case Diagram"
[class diagram php]: https://raw.githubusercontent.com/SAS-Systems/Unveiled-Documentation/master/Bilder/UML%20Class%20diagrams/UML-PHP-Stack_new.png "Class Diagram for our Backend PHP-Stack"
[deployment diagram]: https://raw.githubusercontent.com/SAS-Systems/Unveiled-Documentation/master/Bilder/UML%20Class%20diagrams/UML_deployment.png "Deployment diagram, shows all modules and the relations between them"
[ci lifecycle]: https://raw.githubusercontent.com/SAS-Systems/Unveiled-Documentation/master/Bilder/auto_deployment_lifecycle.png "Continuous Integration process"
