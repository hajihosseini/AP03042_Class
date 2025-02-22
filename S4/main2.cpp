#include<iostream>
using namespace std;

class Date
{
    int day;
    int month;
    int year;
};

class Person
{
    int m_NID;
    char m_fname[20];
    char m_lname[20];
    Date m_BD;

    public:

    Person(const char* fname, const char* lname, int id, Date bd)
    :m_BD(bd), m_NID(id)
    {
        int i;
        for(i=0; i<sizeof(m_fname) && fname[i]!='\0';i++)
        {
            m_fname[i] = fname[i];
        }
        m_fname[i] = '\0';

        for(i=0; i<sizeof(m_lname) && lname[i]!= '\0'; i++)
        {
            m_lname[i] = lname[i];
        }
        m_lname[i] = '\0';

    }

    void Print_person() const
    {
        cout << "Name: " << m_fname << endl;
        cout << "Family: " << m_lname << endl;

    }
};


int main()
{
    Date d;
    Person p1("Ali","hosseini", 123213, d);

    p1.Print_person();

}