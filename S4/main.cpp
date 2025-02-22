#include<iostream>
#include<math.h>
using namespace std;


class Point
{
    public:
    double m_x;
    double m_y;

    Point (){};

    Point (double x, double y): m_x(x),m_y(y){};

    double DistanceTo(const Point &p)const
    {
        double x_diff = m_x - p.m_x;
        double y_diff = m_y - p.m_y;
        return sqrt(x_diff*x_diff + y_diff*y_diff);
    }
    void print_point()
    {
        cout << "x:" <<m_x << ", y: " << m_y << endl;
    }
};

class Circle
{
    public:
    Point m_o;
    double m_r;

    //Circle(const Point& o, double r)
    //{
        //o.m_x = o.m_x +1;
    //    m_o = o;
    //    m_r = r;
    //}

    Circle (const Point&o , double r):m_o(o),m_r(r){};
    Circle () {};

    double DistanceTo(const Circle &c)
    {
        return DistanceTo(c.m_o);
    }

    double DistanceTo(const Point &p)
    {
        return p.DistanceTo(m_o);
    }
};
int main()
{
    Point p1;
    Point p2(4, 7);
    Point p3(1, 5);
    Circle c1(Point (1, 9), 12.3);
    Circle c2(p3, 12);
    cout << c1.DistanceTo(c2) << endl;
    cout << c1.DistanceTo(p1) << endl;
    cout << p1.DistanceTo(p2) << endl;
}