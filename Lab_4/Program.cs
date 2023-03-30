using System;

class Program
{
    // Заданные функции p(x), q(x), f(x)
    static double p(double x) => Math.Cosh(x);
    static double q(double x) => 2;
    static double f(double x) => 1;

    // Начальные условия
    static double A = 0;
    static double B = 1;
    static double C = 2;
    static double D = 0;
    static double E = 1;
    static double F = 2;

    // Число шагов метода Рунге-Кутты
    static int N = 100;

    static void Main()
    {
        // Шаг по x
        double h = (B - A) / N;

        // Массивы для хранения значений y(x) и u(x)
        double[] y = new double[N + 1];
        double[] u = new double[N + 1];

        // Начальные условия
        y[0] = D;
        u[0] = E;

        // Вычисление значений y(x) и u(x) методом Рунге-Кутты
        for (int i = 0; i < N; i++)
        {
            double x = A + i * h;
            double k1y = h * u[i];
            double k1u = h * (f(x) - p(x) * u[i] - q(x) * y[i]);
            double k2y = h * (u[i] + k1u / 2);
            double k2u = h * (f(x + h / 2) - p(x + h / 2) * (u[i] + k1u / 2) - q(x + h / 2) * (y[i] + k1y / 2));
            double k3y = h * (u[i] + k2u / 2);
            double k3u = h * (f(x + h / 2) - p(x + h / 2) * (u[i] + k2u / 2) - q(x + h / 2) * (y[i] + k2y / 2));
            double k4y = h * (u[i] + k3u);
            double k4u = h * (f(x + h) - p(x + h) * (u[i] + k3u) - q(x + h) * (y[i] + k3y));
            y[i + 1] = y[i] + (k1y + 2 * k2y + 2 * k3y + k4y) / 6;
            u[i + 1] = u[i] + (k1u + 2 * k2u + 2 * k3u + k4u) / 6;
        }

        // Вычисление коэффициента при y(B) для случая C = y(B) и E = y'(A)
        double alpha = (F - u[N] * (C - B) / y[N]) / (E + u[0] * (C - A) / y[N]);

        // Вычисление значения y(B) методом линейной интерполяции
        double yB = y[N] + alpha * (y[N] - y[0] * (B - A) / (C - A));

        Console.WriteLine("y(B) = {0}", yB);
    }
}
