#include<vector>
#include<iostream>
#include<string>
#include<stdio.h>
#include<Windows.h>

// RAII
// Resource Acquisition Is Initialization

using namespace std;
class myvector
{
public:
    myvector(int capacity=10)
        : m_capacity(capacity)
        , m_buffer(new int[capacity]) 
        , m_size(0)
    {
        resize(this);
    }

    void resize(myvector* v)
    {

    }

    ~myvector()
    {
        delete [] this->m_buffer;
        this->m_capacity = 0;
    }

    void add(int* buf, int e)
    {
        buf[m_size] = e;
        m_size++;       
    }

    void add(int e)
    {
        add(m_buffer, e);
    }

    int at(int idx)
    {
        return m_buffer[idx];
    }

private:
    int m_capacity;
    int m_size;
    int* m_buffer;    
};

int main2()
{
    myvector v;
    v.add(2);
    v.add(5);
    cout << v.at(0) << " " << v.at(1) << endl;
    
    return 0;
}

class myfile
{
    string m_filename;
    FILE* m_fileHandle;
public:
    myfile(const string& filename)
        : m_filename(filename)
    {
        m_fileHandle = fopen(m_filename.c_str(), "w");
    }
    ~myfile()
    {
        fclose(m_fileHandle);
    }
    void writeln(const string& str)
    {
        fprintf(m_fileHandle, str.c_str());
    }
};
// TODO 1
// KeepTime:
// {
//      KeepTime t("for loop")
//  ..
// ..
//  }

bool inst = false;
int main()
{
    {
        // KeepTime t("for loop 365 double mul");
        double d = 1;
        for(int i=0; i<365; i++)
            d = d * 1.01;
    }

}

int main3()
{
    {
        unsigned long long l1 = GetTickCount64();
        long double d = 1.01;
        for(int i=0; i<365; i++)
            d = d * 1.01;
        unsigned long long l2 = GetTickCount64();

        cout << d << endl;
        cout << l2-l1 << endl;

    }
    // myfile f("test.txt");
    // f.writeln("test\n");
    return 0;
}