#pragma once
using namespace std;

// This is the enum type for the class type
enum class classType {
    honors,
    ap,
    regular
};

// This is the main class that defines how the classes are stored. It has a course name, grade, and class type
class GPA_Class {
private:
    string course_name;
    char grade;
    classType class_type;
public:
    GPA_Class(string course_name, char grade, classType class_type) {
        this->course_name = course_name;
        this->grade = grade;
        this->class_type = class_type;
    }

    string getCourseName() {
        return course_name;
    }

    char getGrade() {
        return grade;
    }

    classType getClassType() {
        return class_type;
    }

    void setCourseName(string course_name) {
        this->course_name = course_name;
    }

    void setGrade(char grade) {
        this->grade = grade;
    }

    void setClassType(classType class_type) {
        this->class_type = class_type;
    }
};