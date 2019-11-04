using System;

public class NormalDistribution
{
    // The polar method
    private bool _hasDeviate;
    private double _storedDeviate;
    private readonly Random _random;

    public NormalDistribution(Random random = null)
    {
        _random = random ?? new Random();
    }

    public double NextGaussian(double mu = 0, double sigma = 1)
    {
        if (sigma <= 0)
            throw new ArgumentOutOfRangeException("sigma", "Must be greater than zero.");

        if (_hasDeviate)
        {
            _hasDeviate = false;
            return _storedDeviate * sigma + mu;
        }

        double v1, v2, rSquared;
        do
        {
            v1 = 2 * _random.NextDouble() - 1;          // two random values between -1.0 and 1.0
            v2 = 2 * _random.NextDouble() - 1;
            rSquared = v1 * v1 + v2 * v2;
        } while (rSquared >= 1 || rSquared == 0);       // ensure within the unit circle

        var polar = Math.Sqrt(-2 * Math.Log(rSquared) / rSquared);  // calculate polar tranformation for each deviate
        _storedDeviate = v2 * polar;
        _hasDeviate = true;
        return v1 * polar * sigma + mu;
    }
}
