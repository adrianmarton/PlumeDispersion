using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoRisk.riskEvaluation
{
    public class Sigma
    {

    public  Boolean FLOTABILITY_EFFECT = false;
    public  float  dH                 = 0.0f;
    public  int     MIN_X              = 1;
    public  int     MAX_X              = 100000;// Measured in m[meter]
    
    
    private float L = 0.0f;
    private float M = 0.0f;
    private float N = 0.0f;
    
     
    
    public float sigmaY(int meter,   StabilityClassEnum  stabilityClass, TerrainType terrain){
        
        float retValue = 0.0f;
                    
        if (terrain == TerrainType.URBAN)
        {
            /**URBAN**/
            switch(stabilityClass) {

                case StabilityClassEnum.A:  
                    retValue = this.sigmaY_Urban_A(meter);
                break;
               
                //A=B
                case StabilityClassEnum.B:
                     retValue = this.sigmaY_Urban_B(meter);
                break;
                
                //C
                case StabilityClassEnum.C:
                     retValue = this.sigmaY_Urban_C(meter);
                break;
                
                //D
                case StabilityClassEnum.D:
                     retValue = this.sigmaY_Urban_D(meter);
                break;
                
                //E=F
                case StabilityClassEnum.E:
                     retValue = this.sigmaY_Urban_E(meter);
                break;
                
                //F=E
                case StabilityClassEnum.F:
                     retValue =  this.sigmaY_Urban_F(meter);
                break;
            }
            
        } else {
            
                /**RURAL**/
                switch(stabilityClass)
                {
                    //A
                    case StabilityClassEnum.A:
                        retValue = this.sigmaY_Rural_A(meter);
                    break;

                    //B
                    case StabilityClassEnum.B:
                        retValue = this.sigmaY_Rural_B(meter);
                    break;

                    //C
                    case StabilityClassEnum.C:
                        retValue = this.sigmaY_Rural_C(meter);
                    break;

                    //D
                    case StabilityClassEnum.D:
                        retValue = this.sigmaY_Rural_D(meter);
                    break;

                    //E
                    case StabilityClassEnum.E:
                        retValue = this.sigmaY_Rural_E(meter);
                    break;

                    //F
                    case StabilityClassEnum.F:
                         retValue = this.sigmaY_Rural_F(meter);
                    break;
                }
        }
        
        //Efectul de flotabilitate;
        if (this.FLOTABILITY_EFFECT) {
            
                retValue += (float)dH/3.5f;
        }
        
        return retValue;
    }
    
    public float sigmaZ(int meter,   StabilityClassEnum  stabilityClass, TerrainType terrain){

            float retValue = 0.0f;
        
        if (terrain == TerrainType.URBAN)
        {
            
            /**URBAN**/
            switch(stabilityClass) {

                case StabilityClassEnum.A:  
                    
                    retValue = this.sigmaZ_Urban_A(meter);
                break;
               
                //A=B
                case StabilityClassEnum.B:
                    
                     retValue = this.sigmaZ_Urban_B(meter);
                break;
                
                //C
                case StabilityClassEnum.C:
                    
                     retValue = this.sigmaZ_Urban_C(meter);
                break;
                
                //D
                case StabilityClassEnum.D:
                  
                     retValue = this.sigmaZ_Urban_D(meter);
                break;
                
                //E=F
                case StabilityClassEnum.E:
                     retValue = this.sigmaZ_Urban_E(meter);
                break;
                
                //F=E
                case StabilityClassEnum.F:
                     
                     retValue =  this.sigmaZ_Urban_F(meter);
                break;
            }
            
        } else {
            
                /**RURAL**/
                switch(stabilityClass)
                {
                    //A
                    case StabilityClassEnum.A:
                        
                        retValue = this.sigmaZ_Rural_A(meter);
                    break;

                    //B
                    case StabilityClassEnum.B:
                        
                        retValue = this.sigmaZ_Rural_B(meter);
                    break;

                    //C
                    case StabilityClassEnum.C:
                        
                        retValue = this.sigmaZ_Rural_C(meter);
                    break;

                    //D
                    case StabilityClassEnum.D:
                        
                        retValue = this.sigmaZ_Rural_D(meter);
                    break;

                    //E
                    case StabilityClassEnum.E:
                        
                        retValue = this.sigmaZ_Rural_E(meter);
                    break;

                    //F
                    case StabilityClassEnum.F:
                        
                         retValue = this.sigmaZ_Rural_F(meter);
                    break;
                }
        }

        if (this.FLOTABILITY_EFFECT) {
            
                retValue += (float)dH/3.5f;
        }
              
        return retValue;
    }

    public float[] sigmaY_Range(int initialValue, int finalValue, StabilityClassEnum  stabilityClass, TerrainType terrain)
    {
        
        float [] retValue  = new float[finalValue-initialValue+2];
        
        for(int i=initialValue;i<finalValue+1;i++) {
            retValue[i-initialValue+1] = sigmaY(i, stabilityClass, terrain);
        }
        
        return retValue;
    }
    
    public float[] sigmaZ_Range(int initialValue, int finalValue, StabilityClassEnum  stabilityClass, TerrainType terrain)
    {
        
        float [] retValue  = new float[finalValue-initialValue+2];
        
        for(int i=initialValue;i<finalValue+1;i++) {
            retValue[i-initialValue+1] = sigmaZ(i, stabilityClass, terrain);
        }
        
        return retValue;
    }
    
    //SIGMA Y, URBAN 
    
    //Y
    private float  sigmaY_Urban_A(int x){
        changeLMN(4);
        return (x > 0) ? sigma(x,L,M,N) : 1.0f;
    }
    private float  sigmaY_Urban_B(int x){
        
        return sigmaY_Urban_A(x);
    }
    private float  sigmaY_Urban_C(int x){
        
        changeLMN(5);
        return (x > 0) ? sigma(x,L,M,N) : 1.0f;
    }
    private float  sigmaY_Urban_D(int x){
        
        changeLMN(6);
        return (x > 0) ? sigma(x,L,M,N) : 1.0f;
    }
    private float  sigmaY_Urban_E(int x){
        
       changeLMN(7);
        return (x > 0) ? sigma(x,L,M,N) : 1.0f;
    }
    private float  sigmaY_Urban_F(int x){
        
        return sigmaY_Urban_E(x);
    }    
    //Z
    private float  sigmaZ_Urban_A(int x){
        
        changeLMN(0);
        return (x > 0) ? sigma(x,L,M,N) : 1.0f;
    }    
    private float  sigmaZ_Urban_B(int x){
        
        return sigmaZ_Urban_A(x);
    }    
    private float  sigmaZ_Urban_C(int x){
        
        changeLMN(1);
        return (x > 0) ? sigma(x,L,M,N) : 1.0f;
    }
    private float  sigmaZ_Urban_D(int x){
        
        changeLMN(2);
        return (x > 0) ? sigma(x,L,M,N) : 1.0f;
    }
    private float  sigmaZ_Urban_E(int x){
        
        changeLMN(3);
        return (x > 0) ? sigma(x,L,M,N) : 1.0f;
    }
    private float  sigmaZ_Urban_F(int x){
       return sigmaZ_Urban_E(x);
    }
   
    
    //SIGMA Y, RURAL
    //Y
    private float  sigmaY_Rural_A(int x){
        
        changeIJK(6);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;
    }
    private float  sigmaY_Rural_B(int x){
        
        changeIJK(7);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;
    }
    private float  sigmaY_Rural_C(int x){
        
        changeIJK(8);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;
    }
    private float  sigmaY_Rural_D(int x){
        
        changeIJK(9);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;
    }
    private float  sigmaY_Rural_E(int x){
        
        changeIJK(10);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;
    }
    private float  sigmaY_Rural_F(int x){
        
        changeIJK(11);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;
    }
    //Z
    private float  sigmaZ_Rural_A(int x){
        
        changeIJK(0);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;
    }
    private float  sigmaZ_Rural_B(int x){
        
        changeIJK(1);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;    
    }
    private float  sigmaZ_Rural_C(int x){
    
        changeIJK(2);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;
    }
    private float  sigmaZ_Rural_D(int x){
        
        changeIJK(3);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;
    }
    private float  sigmaZ_Rural_E(int x){
        
        changeIJK(4);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;
    }
    private float  sigmaZ_Rural_F(int x){
        
        changeIJK(5);
        return (x > 0) ? sigmaRural(x,L,M,N) : 1.0f;    
    }
    
    //SIGMA
    private float sigma(float x,float L,float M, float N) {
        
        
        float X = (float)x/1000; //Km
       
        return (float) ((L*X)*Math.Pow((1.0f+M*X),N));
    }
    
    private float sigmaRural(float x,float L,float M, float N) {
        
        float X = (float)x/1000; //Km
        
        return (float) Math.Pow(Math.E,L+M*Math.Log(X)+N*Math.Log(X)*Math.Log(X));
    }
    
    private void changeLMN(int index) {
      /**                  zAB   zC    zD      zEF    yAB      yC     yD      yEF      **/
       float []l_array = {240f , 200f , 140f  , 80f    , 320f   , 220f   , 160f  , 110f  };
       float []m_array = {1.0f, 0.00f, 0.30f , 1.50f , 0.40f , 0.40f  , 0.40f , 0.40f };
       float []n_array = {0.50f, 0.00f, -0.50f, -0.50f , -0.50f , -0.50f , -0.50f, -0.50f};             
        
       this.L = l_array[index];
       this.M = m_array[index];
       this.N = n_array[index];
    }
    
    private void changeIJK(int index) {
      /**                    zA      zB       zC      zD        zE      zF       yA       yB       yC       yD        yE       yF      **/
       float []i_array = {6.0350f, 4.6940f,  4.1100f,  3.4140f,  3.0570f,  2.6210f,  5.3570f,  5.0580f,  4.6510f,  4.2300f,  3.9220f , 3.5330f};
       float []j_array = {2.1097f, 1.0629f,  0.9201f,  0.7371f,  0.6794f,  0.6564f,  0.8828f,  0.9024f,  0.9181f,  0.9222f,  0.9222f,  0.9191f};
       float []k_array = {0.2770f, 0.0136f, -0.0020f, -0.0316f, -0.0450f, -0.0540f, -0.0076f, -0.0096f, -0.0076f, -0.0087f, -0.0064f, -0.0070f};             
        
       this.L = i_array[index];
       this.M = j_array[index];
       this.N = k_array[index];
    }

    
    
    public int getMIN_X() {
        return MIN_X;
    }

    public void setMIN_X(int MIN_X) {
        this.MIN_X = MIN_X;
    }

    public int getMAX_X() {
        return MAX_X;
    }

    public void setMAX_X(int MAX_X) {
        this.MAX_X = MAX_X;
    }
    
    public Boolean isFLOTABILITY_EFFECT() {
        return FLOTABILITY_EFFECT;
    }

    public void setFLOTABILITY_EFFECT(Boolean FLOTABILITY_EFFECT) {
        this.FLOTABILITY_EFFECT = FLOTABILITY_EFFECT;
    }

    public float getdH() {
        return dH;
    }

    public void setdH(float dH) {
        this.dH = dH;
    }
  
}

    
    public enum TerrainType{RURAL=0,URBAN=1};
    
    public enum StabilityClassEnum{A = 1, B = 2 , C = 3, D = 4, E = 5, F = 6};

    public enum CloudCover{HIGH=1 ,LOW=2};

    public enum IncomeSolarRadiation {Strong = 1,Moderate = 2,Slight = 3} ;

    public class StabilityClass
    {

        public StabilityClassEnum getStabilityClass(double windSpeed, Boolean isNight, IncomeSolarRadiation solarRadiation, CloudCover cloudCover)
        {
            int stabilityClassIndex = -1;
            if (windSpeed < 2)
            {
                stabilityClassIndex = 0;
            }
            else if ((windSpeed >= 2) && (windSpeed < 3))
            {
                stabilityClassIndex = 1;
            }
            else if ((windSpeed >= 3) && (windSpeed < 5))
            {
                stabilityClassIndex = 2;
            }
            else if ((windSpeed >= 5) && (windSpeed < 6))
            {
                stabilityClassIndex = 3;
            }
            else if (windSpeed >= 6)
            {
                stabilityClassIndex = 4;
            }


            StabilityClassEnum[] sClasses = null;
            if (isNight)
            {
                sClasses = getStabilityClassNight(cloudCover);

            }
            else
            {
                sClasses = getStabilityClassDay(solarRadiation);
            }

            return sClasses[stabilityClassIndex];

        }

        private StabilityClassEnum[] getStabilityClassDay(IncomeSolarRadiation solarRadiation)
        {

            StabilityClassEnum[] strong = { StabilityClassEnum.A, StabilityClassEnum.A, StabilityClassEnum.B, StabilityClassEnum.C, StabilityClassEnum.C };
            StabilityClassEnum[] moderate = { StabilityClassEnum.A, StabilityClassEnum.B, StabilityClassEnum.B, StabilityClassEnum.C, StabilityClassEnum.D };
            StabilityClassEnum[] slight = { StabilityClassEnum.B, StabilityClassEnum.C, StabilityClassEnum.C, StabilityClassEnum.D, StabilityClassEnum.D };


            switch (solarRadiation)
            {
                case IncomeSolarRadiation.Strong: return strong;
                case IncomeSolarRadiation.Moderate: return moderate;
                case IncomeSolarRadiation.Slight: return slight;
            }

            return moderate;
        }

        private StabilityClassEnum[] getStabilityClassNight(CloudCover cloudCover)
        {

            StabilityClassEnum[] high = { StabilityClassEnum.E, StabilityClassEnum.E, StabilityClassEnum.D, StabilityClassEnum.D, StabilityClassEnum.D };
            StabilityClassEnum[] low = { StabilityClassEnum.F, StabilityClassEnum.F, StabilityClassEnum.E, StabilityClassEnum.D, StabilityClassEnum.D };


            switch (cloudCover)
            {
                case CloudCover.LOW: return low;
                case CloudCover.HIGH: return high;

            }
            return null;
        }

    }


}




