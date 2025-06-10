using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.DTOs.AttendanceDTO;
using School.DTOs.AttendanceDTOs;
using School.DTOs.ClassDTOs;
using School.DTOs.ClassSubjectDTOs;
using School.DTOs.GradeDTOs;
using School.DTOs.ParentDTOs;
using School.DTOs.StudentDTOs;
using School.DTOs.SubjectDTOs;
using School.DTOs.TeacherClassSubjectDTOs;
using School.DTOs.TeacherDTO;
using School.DTOs.TeacherDTOs;
using School.DTOs.TimetableDTOs;
using School.Models;

namespace School.MappingConfigs
{
    public class MappingConfig:Profile
    {
        public MappingConfig() {
            CreateMap<AddClassDTO, Class>().ReverseMap();
            CreateMap<Class, DisplayClassDTO>().ReverseMap();

            CreateMap<ClassSubject , displayClassSubjectDTO>().AfterMap(
                (src, dest)=>{
                    dest.ClassName = src.Class.Name;
                    dest.SubjectName = src.Subject.Name;
            }).ReverseMap();
            CreateMap<ClassSubject, addClassSubjectDTO>().ReverseMap();
                

            CreateMap<Attendance, displayAttendanceDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.StudentName = src.Student.Name;
                }).
                ReverseMap();
            CreateMap<Attendance, AddAttendanceDTO>().ReverseMap();


            CreateMap<Timetable, addTimetableDTO>().ReverseMap();
            CreateMap<Timetable, displayTimetableDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.TeacherName = src.Teacher?.Name ?? "N/A";
                    dest.SubjectName = src.Subject?.Name ?? "N/A";
                    dest.ClassName = src.Class?.Name ?? "N/A";
                })
                .ReverseMap();


            CreateMap<Subject, displaySubjectDTO>().ReverseMap();
            CreateMap<Subject, addSubjectDTO>().ReverseMap();

            CreateMap<Teacher, displayTeacherDTO>().ReverseMap();
            CreateMap<Teacher, addTeacherDTO>().ReverseMap();

            CreateMap<TeacherClassSubject , displayTeacherClassSubjectDTO>().AfterMap(
                (src, dest) => {
                    dest.TeacherName = src.Teacher.Name;
                    dest.ClassName = src.Class.Name;
                    dest.SubjectName = src.Subject.Name;
                }).ReverseMap();
            CreateMap<TeacherClassSubject , addTeacherClassSubjectDTO>() .ReverseMap();


            CreateMap<Grade, displayGradeDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.SubjectName = src.Subject.Name;
                    dest.StudentName = src.Student.Name;
                }).ReverseMap();
            CreateMap<Grade, addGradeDTO>().ReverseMap();

            CreateMap<Student, displayStudentDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.ClassName = src.Class.Name;
                }
                    ).ReverseMap();
            CreateMap<Student, addStudentDTO>().ReverseMap();

            CreateMap<Parent , displayParentDTO>().ReverseMap();
            CreateMap<Parent , addParentDTO>().ReverseMap();
            
        }
    }
}
