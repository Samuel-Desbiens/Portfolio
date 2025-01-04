class PMSReader
{
public:
    void Setup();
    void Loop();
    int GetData1p0();
    int GetData2p5();
    int GetData10p0();

protected:
private:
    int pmg1;
    int pmg2;
    int pmg10;
};