# Docker Practice

This repository contains exercises related to the Docker platform — building containers, configuring them, and exploring practical use cases.

---

## 🔀 Branches Overview

> _In this section, an explanation of each branch will be provided._  
> *(To be completed as branches are added.)*

---

## 🐳 Dockerfile: Basic Configuration

The application in this repository is forked from the official Microsoft example:  
🔗 [mslearn-hotel-reservation-system](https://github.com/MicrosoftDocs/mslearn-hotel-reservation-system.git)

A `Dockerfile` was created in the `src` directory to containerize the application.

---

### 🚀 Running the Application in Docker

Open a terminal in the `src` directory and execute the following commands:

1. **Build the image**:
   ```bash
   docker build -t reservationsystem .
   ```

2. **List available images**:
   ```bash
   docker image list
   ```

3. **Run a container from the image**:
   ```bash
   docker run -p 8080:80 -d --name reservations reservationsystem
   ```

4. **Access the app in your browser**:  
   Open [http://localhost:8080/api/reservations/1](http://localhost:8080/api/reservations/1)  
   You should see a sample response from the app.

5. **Check container status**:
   ```bash
   docker ps -a
   ```

6. **Stop the container**:
   ```bash
   docker container stop reservations
   ```

7. **Remove the container**:
   ```bash
   docker rm reservations
   ```

---

## 📁 File Structure (Main Branch)

> Instructions above are related to the files available in the `main` branch.

---

Feel free to explore other branches for more advanced Docker scenarios and exercises.
