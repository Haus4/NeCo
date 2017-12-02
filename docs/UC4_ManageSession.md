# NeCo - Use-Case Specification: Manage Session

## 1. Use-Case Name
Manage Session

### 1.1 Brief Description
The app provides the server with the possibility to manage sessions.

### 1.2 Screenshot Mockup
(n/a)
## 2. Flow of Events

### 2.1 Basic Flow

<!--![basic flow] -->


### 2.2 Alternative Flow
(n/a)


## 3. Special Requirements
### 3.1 Use chat protocol: TCP
The Android-App and the Xamarin-Backend should transfer their data with the Transmission Control Protocol (TCP). 

### 3.1. Use encryption library: Signal Protocol
The client uses the encryption library to encrypt the message.


## 4. Preconditions

### 4.1 User key is generated
The chat messages are signed with a PGP-Key, which is generated upon opening the app.

### 4.2 User key is exchanged
The keys must have been exchanged before chating.

### 4.3 User is on the main page
The user should have already opened the NeCo Application on his smartphone and navigated to the main page.


## 5. Postconditions
(n/a)


## 6. Extension Points
(n/a)

## 7. Function Point calculation
(n/a)

<!-- Link definitions: -->

[basic flow]: <link> "Manage Session Basic Flow"

