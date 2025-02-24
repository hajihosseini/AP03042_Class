#include<iostream>
class Student
{
    public:
    char m_name[20];
    int m_stdno;
    double m_grade;

    void add_grade(int x) { m_grade += x;}
    void add_grade(double x) {m_grade += x;}
    void add_grade(char c) {
        if (c == '+')
            m_grade += 0.25;
        else if (c == '*')
            m_grade += 0.5;
        else if (c == '-')
            m_grade -= 0.25;
    }
};

using namespace std;

int main()
{
    Student s;
    Student* ps = &s;
    int x;
    s.m_name[0] = 'A';
    s.m_name[1] = 0;
    s.m_stdno = 403521;
    if (true)
        ;
        
    cout.operator<<(5);
     

//    s.add_grade()
    cout << s.m_name << endl;
}