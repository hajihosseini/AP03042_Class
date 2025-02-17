#include<iostream>
#include<math.h>

using namespace std;

class Point
{
    public:
        double x;
        double y;

        // Point (Point* q)
        // {
        //     x = q->x;
        //     y = q->y;
        // }

        Point (const Point& q) //const correctness
        {
            x = q.x;
            //q.y+=1;
            y = q.y;
        }

        Point(double t, double r)
        {
            x = t;
            y = r;
            cout << "x in c:"<< x <<", y in c:"<<y << endl;

        }

        Point (double w)
        {
            x = w;
            y = w;
        }

        Point ()
        {
            //x=0;
            //y =0;
        }

        
        void print_point()
        {
            cout << "x: " <<x << ", y:" << y<<endl;
        }

        double points_distance(Point w)
        {
            double x_diff = x - w.x;
            double y_diff = y - w.y;

            return sqrt(x_diff*x_diff + y_diff*y_diff);
        }

        ~Point(){
            cout << "x in d:"<< x <<", y in d:"<<y << endl;
        }
};


int main()
{
    Point p1(1,1);
    int c = 5;
    if (c>3)
    {
        Point p2(2,2);
    }
    else{
        Point p3(3,3);
    }

}


int main1()
{
    Point p0(9);
    p0.print_point();
    //Point p1;
    Point p1(4,5);

    //p1.x = 4;
    //p1.y = 5;

    //print_point(p1);
    p1.print_point();

    Point p2;
    p2.print_point();
    p2.x = -5;
    p2.y = 10;


    //print_point(p2);
    p2.print_point();

    Point p3(p2);
    p3.print_point();

    cout << p1.x << endl;

    double r = p1.points_distance(p2);

    cout << r<< endl;


    int a;
    cin>>a;
    cout<<"a:" <<a<<endl;

    return 0;

}