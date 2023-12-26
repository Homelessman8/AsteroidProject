[System.Serializable]
public class SampleDataComplex
{
    public string name;
    public Address address;
    public book[] books;
}

[System.Serializable]
public class Address
{
    public int unit;
    public string road;
    public string city;
}

[System.Serializable]
public class book
{
    public string name;
    public bool isDigital;
    public string author;
}