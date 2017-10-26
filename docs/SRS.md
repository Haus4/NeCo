# NeCo - Software Requirements Specification

## 1. Introduction
### 1.1 Purpose
This SRS describes all specifications for "NeCo". It’s an Xamarin-App. "NeCo" allows
users to chat with people in their near environment. In this document the usage of the
"NeCo"-Xamarin-App will be explained. Furthermore reliability, reaction speed and other important
characteristics of this project will be specified. This includes design and architectural decisions regarding optimization of
these criteria as well.

### 1.2 Scope
This software specification applies to the whole "NeCo" application. The app allows users to chat with strangers in their near environment to meet new people, which are at the same spot as the user. It establishes user- and systemcreated chatrooms among users in close environments. 


### 1.3 Definitions, Acronyms and Abbreviations
In this section definitions and explanations of acronyms and abbreviations are listed to help the reader to understand these.

- **Android** This is a mobile operating system developed by Google which is primarily used on smartphones and tablets.
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
The basic idea behind NeCo is to use geolocalization technologies to create chatrooms with people around you, similar to the popular app “Jodel”.  The difference is that you chat with random people in your local area, to meet, to party, or just to have fun.
For security reasons we’ll use latest encryption technology like RSA & AES encryption. Ideas for future features could be a friend list to keep in contact.

People using our App can chat with strangers in their near environment. 

The following picture shows the overall use case diagram of our software:
![OUCD]

## 3. Specific Requirements
### 3.1 Functionality - Android App
#### 3.1.1 Chat
The app provides the user with the possibility to chat with another person.

#### 3.1.2 Share files
The user is able to share files with the chatroom.

#### 3.1.3 Chat with people nearby
The app allows the user to chat with people in his near environment. This is provided through geolocalization within the functionality of the smartphone.

#### 3.1.4 Chat encrypted
The app provides an encypted chat, where messages get encrypted with RSA and AES.

#### 3.1.5 Moderated Chat
The app provides naming a moderator, who is able to kick or ban users and delete messages.

#### 3.1.6 Manage Profile
The user is able to change his/her nickname and profile picture within the app. Also managing a friendlist by adding and deleting friends is possible.

#### 3.1.7 Pointsystem
The app provides a reward system where the user gets points for joining a chatroom or using the app.


### 3.2 Usability
#### 3.2.1 Smartphone user
The user should know how to use Android as a mobile operating system and how to install and use a mobile application on it. We will provide a installation guide.

### 3.3 Reliability
#### 3.3.1 Server availability
Our own server should ensure a 95% up-time.

Our server is co-hosted at the DHBW so we must rely on their service.

### 3.4 Performance
The sending of the messages and files from one user to another must not guarantee real-time data transfer, because the message and files will not be displayed and watched live. Nevertheless the transfer should not take longer than 5 seconds to ensure fast response times.

### 3.5 Supportability
#### 3.5.1 Language support
We will use the following languages, which will also be well supported in the future:

- C#
- XML

### 3.6 Design Constraints

#### 3.6.1 MVC architecture
Our Android application should implement the MVC pattern.

### 3.7 On-line User Documentation and Help System Requirements
The whole application will be built with an intuitive design, so there shouldn’t be a need for the user to ask us or the program for help. However, we will write our own blog on which users can find information and ask us questions.

### 3.8 Purchased Components
(n/a)

### 3.9 Interfaces
#### 3.9.1 User Interfaces
(tbd) 

<!--

Please consult the different use case descriptions for UI mockups (screenshots) and UI functionality descriptions:

- [UC1: Chat][uc chat]
- [UC3: Local Chat][uc local chat]
- [UC2: Share files][uc share files]
- [UC4: Encrypted chat][uc encrypted chat]
- [UC5: Manage profile][uc manage profile]
- [UC6: Manage friendlist][uc manage friendlist]
- [UC7: Get Points][uc get points]
- [UC8: Manage chatrooms][uc manage chatrooms]
- [UC9: Name moderator][uc name moderator]
- [UC10: Kick/Ban user][uc kick user]
- [UC11: Delete message][uc delete message]

-->

#### 3.9.2 Hardware Interfaces
(tbd)

#### 3.9.3 Software Interfaces
(tbd)

#### 3.9.4 Communications Interfaces
(tbd)

### 3.10 Licensing Requirement
(tbd)

### 3.11 Legal, Copyright and other Notices
(tbd)

### 3.12 Applicable Standards
(tbd)

## 4. Supporting Information
### 4.1 Appendices
You can find any internal linked sources in the chapter References (go to the top of this document). If you would like to know what the current status of this project is please visit the [NeCo Blog][blog].



[Overall Use Case Diagram (OUCD)]: https://github.com/Haus4/NeCo/blob/master/docs/UseCaseDiagramm.jpg "Link to Github"
<!--

[uc chat]: <link einfügen> "Use Case 1: Chat with another User"
[uc local chat]: <link einfügen> "Use Case 2: Chat with another user nearby"
[uc share files]: <link einfügen> "Use Case 3: Share files with another User"
[uc encrypted chat]: <link einfügen> "Use Case 4: Chat encrypted"
[uc manage profile]: <link einfügen> "Use Case 5: Manage profile informations"
[uc manage friendlist]: <link einfügen> "Use Case 6: Manage friends in friendlist"
[uc get points]: <link einfügen> "Use Case 7: Get Points for using the app"
[uc manage chatrooms]: <link einfügen> "Use Case 8: Manage chatrooms"
[uc name moderator]: <link einfügen> "Use Case 9: Name a moderator"
[uc kick user]: <link einfügen> "Use Case 10: Kick/Ban an user"
[uc delete message]: <link einfügen> "Use Case 11: Delete a message"

-->

[blog]: https://necoproject.wordpress.com/ "Neco Blog"
[github]: https://github.com/Haus4/NeCo "Sourcecode hosted at Github"

<!--

[presentation]: <link einfügen> "Final project presentation"
[installation guide]: <link einfügen> "Android App Installation Guide"

-->

<!-- Picture-Link definitions: -->
[OUCD]: https://github.com/Haus4/NeCo/raw/master/docs/UseCaseDiagramm.jpg "Overall Use Case Diagram"

<!--

[deployment diagram]: <link einfügen> "Deployment diagram, shows all modules and the relations between them"
[ci lifecycle]: <link einfügen> "Continuous Integration process"

-->

