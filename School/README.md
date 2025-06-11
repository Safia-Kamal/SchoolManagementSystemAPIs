School Management System - Backend API

✅Overview
The School Management System is a RESTful backend API developed with ASP.NET Core, using Entity Framework Core and SQL Server. It supports multiple user roles (Admin, Teacher, Student, Parent) and provides a structured way to handle education workflows like grading, scheduling, and attendance tracking.

✅Features
Teacher Management – Add, assign subjects/classes, and manage teachers.

Student Management – Register students, assign to classes, and track performance.

Subject Management – Manage school subjects and class-specific subjects.

Timetable System – Assign subjects to classes per day and time.

Grading System – Manage student grades by subject.

Attendance Tracking – Mark and monitor student attendance.

Parent Accounts – Link students to their parents for progress tracking.

Authentication & Authorization – JWT-based login system with role-based access control.

✅Technology Stack
Programming Language: C#

Framework: ASP.NET Core Web API

Database: SQL Server

ORM: Entity Framework Core

Authentication: ASP.NET Identity + JWT

Architecture: Code First + Dependency Injection

Database Schema & Relations
The application uses a Code-First Approach with the following main entities:

✅Teacher
Id, Name, NationalId

Related: TeacherClassSubject, Timetable

✅Student
Id, Name, NationalId, ClassId, ParentId

Related: Class, Parent, Grades, Attendances

✅Class
Id, Name, GradeLevel

Related: Students, ClassSubjects, TeacherClassSubjects, Timetables

✅Subject
Id, Name

Related: ClassSubject, TeacherClassSubject, Grades

✅Timetable
Id, ClassId, SubjectId, TeacherId, DayOfWeek, StartTime, EndTime

Links teacher and subject to a class at a specific time

✅Grade
Id, StudentId, SubjectId, Value

✅Attendance
Id, StudentId, Date, Status (Present / Absent / Excused)

✅Parent
Id, Name, NationalId

Has many students

✅ApplicationUser
Extends ASP.NET Identity

Role (Admin, Student, Teacher, Parent), RelatedId

Installation & Setup
Clone the Repository:

bash
1- git clone https://github.com/Safia-Kamal/SchoolManagementSystemAPIs.git 
2- Open the solution in Visual Studio or VS Code.
3- Configure your database and JWT settings in appsettings.json
4- Apply database migrations: update-database
5- Run the project

✅Testing
You can use Postman or Swagger to test the API endpoints.
 