using System;

[Serializable]
public struct VFactor
{
	public long nom;

	public long den;

	[NonSerialized]
	public static VFactor zero = new VFactor(0L, 1L);

	[NonSerialized]
	public static VFactor one = new VFactor(1L, 1L);

	[NonSerialized]
	public static VFactor pi = new VFactor(31416L, 10000L);

	[NonSerialized]
	public static VFactor twoPi = new VFactor(62832L, 10000L);

	//private static long mask_ = 9223372036854775807L;

	private static long upper_ = 16777215L;

    internal static readonly VFactor MaxValue = new VFactor(upper_, 1);

    public int roundInt
	{
		get
		{
			return (int)IntMath.Divide(this.nom, this.den);
		}
	}

    internal static VFactor Clamp(VFactor v, VFactor min, VFactor max)
    {
        if (v < min) return min;
        else if (v > max) return max;
        else return v;
    }

    public int integer
	{
		get
		{
			return (int)(this.nom / this.den);
		}
	}

	public float single
	{
		get
		{
			double num = (double)this.nom / (double)this.den;
			return (float)num;
		}
	}

	public bool IsPositive
	{
		get
		{
			if (this.nom == 0L)
			{
				return false;
			}
			bool flag = this.nom > 0L;
			bool flag2 = this.den > 0L;
			return !(flag ^ flag2);
		}
	}

	public bool IsNegative
	{
		get
		{
			if (this.nom == 0L)
			{
				return false;
			}
			bool flag = this.nom > 0L;
			bool flag2 = this.den > 0L;
			return flag ^ flag2;
		}
	}

	public bool IsZero
	{
		get
		{
			return this.nom == 0L;
		}
	}

	public VFactor Inverse
	{
		get
		{
			return new VFactor(this.den, this.nom);
		}
	}

	public VFactor(long n, long d)
	{
		this.nom = n;
		this.den = d;
	}

    // public VFactor(float f)
    // {
    //     this.nom = (int)Math.Round((double)(f * 100));
    //     this.den = 100;
    // }

    public static implicit operator VFactor(int i)
    {
        return new VFactor(i, 1);
    }

    public static implicit operator VFactor(float f)
    {
		var val = (int)Math.Round(f * 100);
        return new VFactor(val, 100);
    }

    public override bool Equals(object obj)
	{
		return obj != null && base.GetType() == obj.GetType() && this == (VFactor)obj;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

    public override string ToString()
    {
        return this.roundInt.ToString();
    }

    public string ToString(string format)
	{
		return this.single.ToString(format);
	}

	public void strip()
	{
		//while ((this.nom & VFactor.mask_) > VFactor.upper_ && (this.den & VFactor.mask_) > VFactor.upper_)
		//{
		//	this.nom >>= 1;
		//	this.den >>= 1;
		//}

        if(this.nom != 0)
        {
            while ((this.nom % 2) == 0 && (this.den % 2) == 0)
            {
                this.nom >>= 1;
                this.den >>= 1;
            }
        }

        while (Math.Abs(this.nom) > 10000 && Math.Abs(this.den) > 10000)
        {
            this.nom /= 10;
            this.den /= 10;
        }

    }

	public static bool operator <(VFactor a, VFactor b)
	{
        a.strip();
        b.strip();

        long num = a.nom * b.den;
		long num2 = b.nom * a.den;
		bool flag = b.den > 0L ^ a.den > 0L;
		return (!flag) ? (num < num2) : (num > num2);
	}

	public static bool operator >(VFactor a, VFactor b)
	{
        a.strip();
        b.strip();

        long num = a.nom * b.den;
		long num2 = b.nom * a.den;
		bool flag = b.den > 0L ^ a.den > 0L;
		return (!flag) ? (num > num2) : (num < num2);
	}

	public static bool operator <=(VFactor a, VFactor b)
	{
        a.strip();
        b.strip();

        long num = a.nom * b.den;
		long num2 = b.nom * a.den;
		bool flag = b.den > 0L ^ a.den > 0L;
		return (!flag) ? (num <= num2) : (num >= num2);
	}

	public static bool operator >=(VFactor a, VFactor b)
	{
        a.strip();
        b.strip();

        long num = a.nom * b.den;
		long num2 = b.nom * a.den;
		bool flag = b.den > 0L ^ a.den > 0L;
		return (!flag) ? (num >= num2) : (num <= num2);
	}

	public static bool operator ==(VFactor a, VFactor b)
	{
        a.strip();
        b.strip();

        return a.nom * b.den == b.nom * a.den;
	}

	public static bool operator !=(VFactor a, VFactor b)
	{
        a.strip();
        b.strip();

        return a.nom * b.den != b.nom * a.den;
    }

    //public static bool operator <(VFactor a, long b)
    //{
    //       a.strip();

    //       long num = a.nom;
    //	long num2 = b * a.den;
    //	return (a.den <= 0L) ? (num > num2) : (num < num2);
    //}

    internal static VFactor Min(VFactor a, VFactor b)
    {
        if(a < b)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    internal static VFactor Max(VFactor a, VFactor b)
    {
        if (a > b)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

 //   public static bool operator >(VFactor a, long b)
	//{
 //       a.strip();

 //       long num = a.nom;
	//	long num2 = b * a.den;
	//	return (a.den <= 0L) ? (num < num2) : (num > num2);
	//}

	//public static bool operator <=(VFactor a, long b)
	//{
	//	long num = a.nom;
	//	long num2 = b * a.den;
	//	return (a.den <= 0L) ? (num >= num2) : (num <= num2);
	//}

	//public static bool operator >=(VFactor a, long b)
	//{
	//	long num = a.nom;
	//	long num2 = b * a.den;
	//	return (a.den <= 0L) ? (num <= num2) : (num >= num2);
	//}

	//public static bool operator ==(VFactor a, long b)
	//{
	//	return a.nom == b * a.den;
	//}

	//public static bool operator !=(VFactor a, long b)
	//{
	//	return a.nom != b * a.den;
	//}

	public static VFactor operator +(VFactor a, VFactor b)
	{
        if (a.IsZero) return b;
        if (b.IsZero) return a;

        a.strip();
        b.strip();

        var ret = new VFactor
		{
			nom = a.nom * b.den + b.nom * a.den,
			den = a.den * b.den
		};

        ret.strip();

        return ret;
    }

	//public static VFactor operator +(VFactor a, long b)
	//{
 //       if (a.IsZero) return b;
 //       if (b == 0) return a;

 //       a.nom += b * a.den;

 //       a.strip();

 //       return a;
	//}

	public static VFactor operator -(VFactor a, VFactor b)
	{
        if (a.IsZero) return -b;
        if (b.IsZero) return a;

        a.strip();
        b.strip();

        var ret = new VFactor
        {
            nom = a.nom * b.den - b.nom * a.den,
            den = a.den * b.den
        };

        ret.strip();

        return ret;
    }

    public static VFactor operator *(VFactor a, VFactor b)
    {
        if (a.IsZero || b.IsZero) return zero;

        a.strip();
        b.strip();

        var ret = new VFactor
        {
            nom = a.nom * b.nom,
            den = a.den * b.den
        };

        ret.strip();

        return ret;
    }

    public static VFactor operator /(VFactor a, VFactor b)
    {
        if(b.IsZero)
        {
            throw new Exception(); 
        }

        if (a.IsZero)
        {
            return zero;
        }

        if (a.nom == b.nom && a.den == b.den) return VFactor.one;

        if (a.nom == -b.nom && a.den == b.den) return new VFactor(-1, 1);

        if (a.nom == b.nom && a.den == -b.den) return new VFactor(-1, 1);

        a.strip();
        b.strip();

        var ret = new VFactor
        {
            nom = a.nom * b.den,
            den = a.den * b.nom
        };

        ret.strip();

        return ret;
    }

 //   public static VFactor operator -(VFactor a, long b)
	//{
 //       if (a.IsZero) return -b;
 //       if (b == 0) return a;

 //       a.nom -= b * a.den;

 //       a.strip();

 //       return a;
	//}

	public static VFactor operator *(VFactor a, long b)
	{
        if (a.IsZero) return 0;
        if (b == 0) return 0;

        a.strip();

        a.nom *= b;

        a.strip();

        return a;
	}

	public static VFactor operator /(VFactor a, long b)
	{
        if (a.IsZero) return 0;
        if (b == 0)
        {
            throw new NotFiniteNumberException("vfactor a/b but b is Nan");
        }

        a.strip();

        a.den *= b;

        a.strip();

        return a;
	}

	public static VInt3 operator *(VInt3 v, VFactor f)
	{
        if (v == VInt3.zero) return VInt3.zero;
        if (f.IsZero) return VInt3.zero;

        f.strip();

        return IntMath.Divide(v, f.nom, f.den);
	}

	public static VInt2 operator *(VInt2 v, VFactor f)
	{

        if (v == VInt2.zero) return VInt2.zero;
        if (f.IsZero) return VInt2.zero;

        f.strip();

        return IntMath.Divide(v, f.nom, f.den);
	}

	public static VInt3 operator /(VInt3 v, VFactor f)
	{
        if (v == VInt3.zero) return VInt3.zero;

        if (f.IsZero)
        {
            throw new NotFiniteNumberException("vfactor a/b but b is Nan");
        }

        f.strip();

        return IntMath.Divide(v, f.den, f.nom);
	}

	public static VInt2 operator /(VInt2 v, VFactor f)
	{
        if (v == VInt2.zero) return VInt2.zero;

        if (f.IsZero)
        {
            throw new NotFiniteNumberException("vfactor a/b but b is Nan");
        }

        f.strip();

        return IntMath.Divide(v, f.den, f.nom);
	}

	//public static int operator *(int i, VFactor f)
	//{
	//	return (int)IntMath.Divide((long)i * f.nom, f.den);
	//}

	public static VFactor operator -(VFactor a)
	{
		a.nom = -a.nom;
		return a;
	}

    //internal static VFactor Pow(VFactor v1, int v2)
    //{
    //    if (v1.IsZero) return 0;
    //    if (v2 == 0) return 1;

    //    if (v2 < 0)
    //    {
    //        throw new NotFiniteNumberException("Pow (v, n) but n < 0");
    //    }

    //    var ret = new VFactor
    //    {
    //        nom = (long)Math.Round(Math.Pow((double)v1.nom, v2)),
    //        den = (long)Math.Round(Math.Pow((double)v1.den, v2)),
    //    };

    //    ret.strip();

    //    return ret;
    //}
}

