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
| [Overall Use Case Diagram (OUCD)](https://github.com/Haus4/NeCo/raw/master/docs/UseCaseDiagramm.jpg)| 19.10.2017 |

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


## 3. Specific Requirements
### 3.1 Functionality - Android App
#### 3.1.1 Chat
The app provides the user with the possibility to chat with another person.
-------------------

#### 3.1.2 Switch user
On this page the user is able to type in his/her login information.
If somebody has multiple users and log in information, the application allows him to switch between them as well. To see the use case diagram
of this use case go to this [document][uc switch user].

#### 3.1.3 Take Picture
The user is able to take pictures with the smartphone camera. The picture will be instantly uploaded to a server.

#### 3.1.4 Take Video
The user is able to film with the smartphone camera. The captured video material is live streamed to a server to store the
data. There is a detached [document][uc capture video] describing this use case more precisely.

#### 3.1.5 Upload File
If the automatic upload of the video or picture failed, the user is able to upload the file manually. This use case is described in [this document][uc upload file].


### 3.2 Functionality - Website
#### 3.2.1 Register for a new account
At the home page of the website there is the possibility to register for a new user. A registration is approved by verifying the user e-mail address. For more information please consider [this document][uc approve registration].

#### 3.2.2 Login-Page
The website contains a login-form for users that have already registered for an account.

#### 3.2.3 Maintain Profile
This overview page allows the user to view and maintain his profile. A more detailed description can be found [here][uc switch user].

#### 3.2.4 Media Browser
At this page all captured and uploaded media of a single user is shown. He can browse the content easily.

#### 3.2.5 Content viewer
Each uploaded file is shown in the media browser. Detailed information is shown below and in a modal dialog the video can be watched. See [this document][uc view own media] for further information.

#### 3.2.6 Delete files
The user is able to delete his own media stored on the server. [Use case document][uc delete own media].

#### 3.2.7 Download files
Captured media can be downloaded only by the owner. Therefore he is able to select specific files in the media browser for download. [Use case document][uc download own media].

#### 3.2.8 Approve registration
Users have to verify their e-mail address to approve their account. [Use case document][uc approve registration].

#### 3.2.9 Manage Users
The administrator is able to perform several different functions on user data. You can read more about this use case [here][uc manage users].

#### 3.2.10 Manage Media
Inappropriate uploads can be deleted by the administrator.

### 3.3 Usability
#### 3.3.1 Smartphone user
The user should know how to use Android as an mobile operating system and how to install and use an mobile application on it. We will provide a installation guide.

#### 3.3.2 Using a browser
The user of our website has to know how to open and work with a modern browser like Chrome, Firefox or Opera.

#### 3.3.3 Honest person
We expect the user to be a honest person, who just upload media that makes our world a better place. Our users should obey the law.

### 3.4 Reliability
#### 3.4.1 Server availability
Our own server should ensure a 90% up-time.

We will also provide an installation-kit so that every institution can host their own streaming server application. In that case the server availability is under the institutions responsibility.

#### 3.4.2 MTTR
This is also in the hands of the server owners.

#### 3.4.3 Compliant to RFCs
Our streaming server and our AndroidApp should be compliant to [RFC 2326][](RTSP) and [RFC 3550][](RTP) to be easily scalable and to ensure a stable and performant stream (also to other clients and servers if required).

### 3.5 Performance
The streaming of the media files from the Android app to the server must not guarantee real-time data transfer, because the file will not be displayed and watched live. It is just saved on the server side. But the data transfer should not take more time than twice the time of the recording had taken.

### 3.6 Supportability
#### 3.6.1 Language support
We will use the following languages, which will also be well supported in the future:

- Java EE 7 (Work on Java EE 8 is already in progress)
- Android 6.0 (Marshmallow)
- the well-known Internet Standards HTML5, CSS3 and JavaScript
- PHP version 5

#### 3.6.2 Support for dependencies
We will build our own RTSP streaming library compliant to [RFC 2326][] and [RFC 3550][] in Java for our backend. Therefore we can ensure that this library is compatible with our streaming application at any time. At client side we will use libstreaming that is hosted by Github. You can find the source code and the description at their [Github-Page][libstreaming]. This library may not be supported in the future, but at this time it will be possible to us our own streaming library in the Android app as well.

Our website frontend uses Angular and jQuery for displaying the media browser and managing the HTML DOM-manipulations.

### 3.7 Design Constraints
All information about the architectural design of our application stack can be found in our [software architecture document][sad]. In the following subchapters you can read about some generall important decisions.

#### 3.7.1 Backend in Java and PHP
The backend of this software should be written in PHP and Java. The PHP-stack is responsible for a RESTful API that is used both by our webinterface and by the Android application. The Java-stack implements a powerful streaming server that uses the RTP and RTSP protocol as already mentioned.

#### 3.7.2 MVC architecture
Our Android application should implement the MVC pattern.

### 3.8 On-line User Documentation and Help System Requirements
The whole application will be built with an intuitive design, so there shouldn’t be a need for the user to ask us or the program for help. However we will write our own blog, on which users can find information and ask us questions.

### 3.9 Purchased Components
(n/a)

### 3.10 Interfaces
#### 3.10.1 User Interfaces
Please consult the different use case descriptions for UI mockups (screenshots) and UI functionality descriptions:

- [UC1: Capture and stream video][uc capture video]
- [UC3: Maintain user profile][uc maintain profile]
- [UC2: Configure settigns][uc configure settings]
- [UC4: Switch user][uc switch user]
- [UC5: Register][uc register]
- [UC6: Browse own media][uc browse media]
- [UC7: Manage Users][uc manage users]
- [UC8: Delete own media][uc delete own media]
- [UC9: Download own media][uc download own media]
- [UC10: View own media][uc view own media]
- [UC11: Approve registration][uc approve registration]
- [UC12: Upload file][uc upload file]

#### 3.10.2 Hardware Interfaces
(n/a)

#### 3.10.3 Software Interfaces
(n/a)

#### 3.10.4 Communications Interfaces
(n/a)

### 3.11 Licensing Requirement
Our server side code is subject to the [Apache Licence 2.0](http://www.apache.org/licenses/LICENSE-2.0) as the libraries used in this application part.

### 3.12 Legal, Copyright and other Notices
(n/a)

### 3.13 Applicable Standards
RFCs:

- [RFC 3550][] - RTP: A Transport Protocol for Real-Time Applications
- [RFC 1889][] - RTP: A Transport Protocol for Real-Time Applications
- [RFC 2326][] - Real Time Streaming Protocol (RTSP)

## 4. Supporting Information
### 4.1 Appendices
You can find any internal linked sources in the chapter References (go to the top of this document). If you would like to know what the current status of this project is please visit the [Unveiled Blog][blog].


<!-- Link definitions: -->
[Edward Snowden]: http://www.brainyquote.com/quotes/quotes/e/edwardsnow551870.html
[Overall Use Case Diagram (OUCD)]: https://github.com/SAS-Systems/Unveiled-Documentation/blob/master/Bilder/UC_Diagrams/Unveiled_Overall%20Use%20Case%20Diagram.png "Link to Github"

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
