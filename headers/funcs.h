#pragma once
#include "classes.h"
using namespace std;

// Definitions for the functions used in main.cpp
string strToLower(const string& str);
void saveClasses(const string& filename, const vector<GPA_Class*>& classes);
void addClass(vector<GPA_Class*>& classes);
void removeClass(vector<GPA_Class*>& classes);
void editClass(GPA_Class& gpa_class);
void listClasses(const vector<GPA_Class*>& classes);
void calculateGPA(const vector<GPA_Class*>& classes, bool weighted);