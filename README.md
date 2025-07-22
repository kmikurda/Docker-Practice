# 🐳 Docker Practice –  3 Containers Application

This branch contains a .NET 8 application that utilizes PostgreSQL and Redis containers. The .NET app includes a Dockerfile that defines how to build the image. PostgreSQL and Redis images are pulled directly from Docker Hub.

---

## .NET Application

The application is as simple as possible — it only registers Redis and PostgreSQL services and provides a few endpoints to verify that communication between containers works correctly.

### 🛠️ Dockerfile

Docker supports multi-stage builds, which enhance transparency and reduce final image size. Docker recommends using a 4-stage build process by default:

| Stage   | Description                                                                 |
|---------|-----------------------------------------------------------------------------|
| Restore | Mainly for documentation and early configuration (e.g., setting environment variables). |
| Build   | Restores NuGet packages and builds the application and its dependencies.    |
| Publish | Publishes the compiled app to a dedicated folder.                          |
| Final   | Builds the final runtime image.                                             |

This separation allows the code to be compiled using a larger SDK image while running the app on a lightweight runtime image, effectively reducing the production image size.

> ⚠️ **Warning:**  
> The provided Dockerfile does **not** configure a non-root user for running the application. Running containers as the default `root` user can pose security risks, such as increased vulnerability to privilege escalation attacks or accidental modification of system files.  
> 
> In production environments, it's recommended to create and use a dedicated user within the Dockerfile to minimize these risks.  
> 
> **Note:** For this simple test/demo scenario, the lack of user configuration is not critical, but keep this best practice in mind for real-world deployments.

---

## 🐘 PostgreSQL

In this example, we use the official `postgres:14-alpine` image from Docker Hub — a lightweight version of PostgreSQL.

---

## 🧠 Redis

As with PostgreSQL, we use the lightweight `redis:7-alpine` image.

---

## 📄 docker-compose

Below are the key sections of the `docker-compose.yml` file:

- **Version**: Indicates the docker-compose version in use. This helps determine which features are supported.
- **Services**: Defines the containerized services:
  - **web**: Our .NET application. Specifies the path to the Dockerfile, sets environment variables for connection strings, defines dependencies on other services, and maps container ports to host ports so the application can be accessed via `localhost`.
  - **db**: PostgreSQL. Requires user, password, and database name configuration. To persist data, a volume should be configured (as shown in this setup).
  - **redis**: A simple Redis instance using the specified image.
- **Volumes**: Registers named volumes. Without this section, Docker Compose may throw an error due to the undefined `postgres_data` volume.  
  
> If you prefer not to define named volumes, you can use an anonymous volume by removing `postgres_data` from the volume path. It should look like: `/var/lib/postgresql/data`.

---

## ▶️ How to Run It

1. Open a terminal in the directory where `docker-compose.yml` is located.
2. Run the command: `docker-compose up --build`
3. All containers should start.
4. Visit the following URLs to verify everything is working:
   - `http://localhost:5000`
   - `http://localhost:5000/db`
   - `http://localhost:5000/redis`

---

Feel free to explore other branches for more advanced Docker scenarios and exercises.
