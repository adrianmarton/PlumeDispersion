using EcoRisk.riskEvaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoRisk.plumeDispersion
{
  public class GaussianDispersionAlgorithm  {
      
       
        //Changed from here
  
    private    float concPoluantEmisie   = 21630000.0f;
    private    float diametrulCosului    = 1.4f;
    private    float inaltimeaCosului    = 76.0f;
    private    float tempAtmosferei      = 15.0f;
    private    float tempGazului         = 204.0f;
    private    float vitezaGazului       = 4.032f;
    private    float vitezaVantului      = 2.0f;
    private    Sigma    sigma  = new Sigma();
    private    Boolean isSolid = false;
    private    float [,]matrix ;
    private    float sedimentationSpeed;
    
    private    float Qf;
    private    float F;
    private    float dH;
    private    float dHf;
    private    float x;
    private    float xf;
    private    float S;
    private    float TCritic;
    private    float u;
    private    float Ht;
    private    float a;
    private    float max;
    private    float min;
    private    float returnLength;
    private    float   Height;
    private int count=0;
    
    private  TerrainType terrainType;
    private  StabilityClassEnum stabilityClass;
        

    float       [] ATM         =  {0.10f,0.15f,0.20f,0.25f,0.25f,0.30f};
    int          atm;//           =    StabilityClassEnum.getValue();
        
    public GaussianDispersionAlgorithm(TerrainType terrainType,StabilityClassEnum stabilityClass,int height, int width)
    {
        this.terrainType = terrainType;
        this.stabilityClass = stabilityClass;
        init(terrainType,stabilityClass);
        matrix  = new float[height,width];
        this.isSolid = isSolid;
        
    }
    
 
    private void init( TerrainType terrainType, StabilityClassEnum stabilityClass ){
        
     
     tempGazului    += 273.14999999999998f; //transformare in Kelvin
     tempAtmosferei += 273.14999999999998f; //transformare In Kelvin
     
     Qf       = (float) ((Math.PI*Math.Pow(diametrulCosului,2.0f)) / (4.0f*vitezaGazului));
     F        = (float) (9.8f*vitezaGazului*Math.Pow(diametrulCosului,2.0f)*((float)(tempGazului-tempAtmosferei) /( 4.0f*tempGazului)));
     dH       = 0.0f;
     dHf      = 0.0f;
     x        = 0.0f;
     xf       = 0.0f;
     S        = 0.0f;
     TCritic  =  273.14999999999998f;
     u        = 0.0f; //viteza vantului m/s
     Ht       = 0.0f;
     a        = 0.0f;
     max      = float.MinValue;
     min      = float.MaxValue;
     atm      = getStabilityClassValue();
     
     
    
     //returnLength = distantaMaximaSR - distantaMinimaSR+1;
     /*
      if ( terrainType == TerrainType.RURAL ) {
            
            ATM_VALUES = ATM_RURAL;
            
       } else {
            
            ATM_VALUES = ATM_URBAN;
       }
       */
   //   Height = riseOfBentoverPlume(stabilityClass,1);
    }
    
   
   private int getStabilityClassValue()
   {
         switch (stabilityClass)
         {
            case StabilityClassEnum.A:
                return 1;
            case StabilityClassEnum.B:
                return 2;
            case StabilityClassEnum.C:
                return 3;
            case StabilityClassEnum.D:
                return 4;
            case StabilityClassEnum.E:
                return 5;
            case StabilityClassEnum.F:
                return 6;
         }
         
         return 0;
    }


    private float getDeltaMaxhight(StabilityClassEnum stabilityClass,int distance){
        
        float retValue = 0.0f;
        float vitezaVant= 0.0f;
        
        if (this.F>=55.0f) this.xf = (float) (119f*Math.Pow(this.F, 0.4f));
        else               this.xf = (float) (49f*Math.Pow(this.F,0.625f));
        
        vitezaVant = vitezaVantului * (float)Math.Pow(inaltimeaCosului/10.0f,ATM[getStabilityClassValue()-1]);
        
        if (getStabilityClassValue()<=3) {
            
            
            if (distance>=(int)this.xf)
                if (this.F>=55.0f)
                    retValue = (float) (38.7f*Math.Pow(F,0.60f)*(1.0f/vitezaVant));
                else
                    retValue = (float) (21.4f*Math.Pow(F,0.75f)*(1.0f/vitezaVant));
        } else {
            
            if (stabilityClass == StabilityClassEnum.E) this.S = (float) 0.02053f;
            if (stabilityClass == StabilityClassEnum.F) this.S = (float) 0.0351839f;
            
            
            if ( (1.84*vitezaVant/Math.Pow(this.S,0.5D))>=xf ){
                if (distance>=(int)this.xf)
                    if (this.F>=55.0)
                        retValue = (float) (38.7f*Math.Pow(F,0.60f)*(1.0f/vitezaVant));
                    else
                        retValue = (float) (21.4f*Math.Pow(F,0.75f)*(1.0f/vitezaVant));
            } else  {
                
                 retValue = (float) (2.4f*Math.Pow(F/vitezaVant*S,0.333f));
            }
                
        }
             
        return retValue;
    }
    private  float riseOfBentoverPlume(StabilityClassEnum stabilityClass,int distance){
    
        
        float aux   = 0.0f;
        float temp  = 0.0f;
        float vitezaVant = this. vitezaVantului*(float)Math.Pow(inaltimeaCosului/10.0d,ATM[getStabilityClassValue()-1]);
       
        
            
        if (this.F>=55.0f) this.xf = (float) (119.0f*Math.Pow(this.F, 0.4f));
        else               this.xf = (float) (49.0f*Math.Pow(this.F,0.625f));
        
        if (stabilityClass == StabilityClassEnum.E) this.S = 0.02053f;
        if (stabilityClass == StabilityClassEnum.F) this.S = 0.0351839f;
        
        if (stabilityClass == StabilityClassEnum.E || stabilityClass == StabilityClassEnum.F) {
            
            aux = (float) (1.84f*vitezaVant*Math.Pow(this.S, (float)-1.0f/2.0f));
            
            if (aux.CompareTo(this.xf) == 0  || aux.CompareTo(this.xf)>0) {
                
                if (distance.CompareTo(xf)<0) {
                    
                    this.dH = (float) (1.6f *Math.Pow(F, (float)1.0f/3.0f)*Math.Pow(distance, (float)2.0f/3.0f)*Math.Pow(vitezaVant, (float)-1.0f));
               
                } else {
                
                    this.dH = (float) (1.6f *Math.Pow(F, (float)1.0f/3.0f)*Math.Pow(this.xf, (float)2.0f/3.0f)*Math.Pow(vitezaVant, (float)-1.0f));
                }
                
            } else {
             
                  if (distance.CompareTo((Math.Pow(this.S, (float)-1.0f/2.0f)*vitezaVant*1.84f)) < 0) {
                      
                      this.dH = (float) (1.6f *Math.Pow(this.F, (float)1.0f/3.0f)*Math.Pow(distance, (float)2.0f/3.0f)*Math.Pow(vitezaVant, (float)-1.0f));
               
                  } else {
                      
                      this.dH = (float) (2.4f*Math.Pow(this.F/(vitezaVant*this.S), (float)1.0f/3.0f));
                  }
            }
            
        } else {
                       
                if (distance.CompareTo(xf)<0) {
       
                    
                    this.dH = (float) (1.6f *Math.Pow(F, (float)1.0f/3.0f)*Math.Pow(distance, (float)2.0f/3.0f)*Math.Pow(vitezaVant, (float)-1.0f));
               
                    //System.out.println("dh"+this.dH);
                    
                } else {
                
                    this.dH = (float) (1.6f *Math.Pow(F, (float)1.0f/3.0f)*Math.Pow(this.xf, (float)2.0f/3.0f)*Math.Pow(vitezaVant, (float)-1.0f));
                }
        }
        
        Ht = dH;

        return Ht;
    }
    
    public float Concentratia(int x, int y, int z) 
    {
        float   retValue = 0.0f;
        float   sigmaY = sigma.sigmaY(x, stabilityClass, terrainType);
        float   sigmaZ = sigma.sigmaZ(x, stabilityClass, terrainType);
        float   inaltime = getDeltaMaxhight(stabilityClass, x);
        
        if (inaltime <= 0.0f){
            
            inaltime = riseOfBentoverPlume(stabilityClass, x);
        }
        
        inaltime += inaltimeaCosului;
       
        float   vitezaVant = vitezaVantului * (float)Math.Pow(inaltime/10.0f,ATM[getStabilityClassValue()-1]);
        float   eq1 = (float) (concPoluantEmisie/ (Math.PI*vitezaVant*sigmaY *sigmaZ));
        float   eq2 = (float) (1.0f / Math.Exp(0.5f*Math.Pow(y/sigmaY, 2.0f)));
        float   eq3 = (float) (1.0f / Math.Exp(0.5f*Math.Pow(inaltime/sigmaZ, 2.0f)));
        float   eq_solid = (float) Math.Exp( -(float)Math.Pow(z-(Height- (sedimentationSpeed*x)/vitezaVantului) ,(float)2.0f)/(2.0f*sigmaZ*sigmaZ) ) ;
                                     
        
        if (isSolid) {
            
            retValue = eq1* eq_solid;
            
        } else {
                    retValue  =  eq1*eq2*eq3;
                   
                    if ( float.IsNaN(retValue) || float.IsNegativeInfinity(retValue) || float.IsPositiveInfinity(retValue)) {
                        retValue = 0.0f;
                        //System.out.println("X:"+ x+ " Y:"+y +"Z:"+z);
                    }
                }
        
        
          if ( float.IsNaN(retValue) || float.IsNegativeInfinity(retValue) || float.IsPositiveInfinity(retValue)) 
          {
                        retValue = 0.0f;
                        //System.out.println("X:"+ x+ " Y:"+y +"Z:"+z);
          }
        
        if (retValue!=0.0f ) {
            
            if (retValue.CompareTo(max )>0) max = retValue;
            if (retValue.CompareTo(min )<0) min = retValue;
        
        } else         
        
        if(retValue.CompareTo(Math.Pow(10.0f, -20.0f)) < 0) {
             
            retValue = (float) 0.0f;
           
        }
    
         return retValue;
        
    }  
 
    
    public float getMin()
    {
        return this.min;
    }
    
    public float getMax(){
        return this.max;
    }
 
    public int getCount(){
        return count;
    }

    public float getDiametrulCosului() {
        return diametrulCosului;
    }

    public void setDiametrulCosului(float diametrulCosului) {
        this.diametrulCosului = diametrulCosului;
    }

    public float getInaltimeaCosului() {
        return inaltimeaCosului;
    }

    public void setInaltimeaCosului(float inaltimeaCosului) {
        this.inaltimeaCosului = inaltimeaCosului;
    }

    public float getTempAtmosferei() {
        return tempAtmosferei;
    }

    public void setTempAtmosferei(float tempAtmosferei) {
        this.tempAtmosferei = tempAtmosferei;
    }

    public float getTempGazului() {
        return tempGazului;
    }

    public void setTempGazului(float tempGazului) {
        this.tempGazului = tempGazului;
    }

    public float getVitezaGazului() {
        return vitezaGazului;
    }

    public void setVitezaGazului(float vitezaGazului) {
        this.vitezaGazului = vitezaGazului;
    }

    public float getVitezaVantului() {
        return vitezaVantului;
    }

    public void setVitezaVantului(float vitezaVantului) {
        this.vitezaVantului = vitezaVantului;
    }

    public Boolean isIsSolid() {
        return isSolid;
    }

    public void setIsSolid(Boolean isSolid) {
        this.isSolid = isSolid;
    }

    public float getSedimentationSpeed() {
        return sedimentationSpeed;
    }

    public void setSedimentationSpeed(float sedimentationSpeed) {
        this.sedimentationSpeed = sedimentationSpeed;
    }

    public float getConcPoluantEmisie() {
        return concPoluantEmisie;
    }                                               s

    public void setConcPoluantEmisie(float concPoluantEmisie) {
        this.concPoluantEmisie = concPoluantEmisie;
    }
    
    
}

/*
    
    
    public float inaltimeaPeneiDeFum(StabilityClassEnum stabilityClass)
    {
      for(x=0.0;x<2000;x+=1.0)
      {
            u  = vitezaVantului* Math.Pow(ATM_VALUES[stabilityClass.getValue()-1], (float)(Ht/10));

            if (stabilityClass.getValue() <= 4) {

                if ( Double.compare(F , 17.61)<0)   TCritic = (0.0297 *tempGazului* Math.Pow(vitezaGazului, 0.333))/ Math.Pow(diametrulCosului,0.667);
                else                                TCritic = (0.00575*tempGazului* Math.Pow(vitezaGazului, 0.667))/ Math.Pow(diametrulCosului,0.333);

            } else {

                if (stabilityClass == StabilityClassEnum.E)  TCritic = 0.00867* Math.sqrt(tempAtmosferei)* vitezaGazului;
                else           TCritic = 0.01147* Math.sqrt(tempAtmosferei)* vitezaGazului;

            }

            if ( Double.compare(TCritic ,(float)(tempGazului-tempAtmosferei))<0) 
            {

                //Algoritm efect terminc


                dH      = (2.34/ vitezaVantului) * Math.Pow(F, 0.333)*Math.Pow(x,0.667);
                dHf     = (1.56/ vitezaVantului) * Math.Pow(F, 0.333)*Math.Pow(x,0.667);

                if (stabilityClass.getValue() <= 4) {

                    if (Double.compare(F, 17.61)<0) {

                        dHf = (50.32 * Math.Pow(F,0.75))  / vitezaVantului;
                        xf  = 100 * Math.Pow(F, 0.625);

                    } else {

                        dHf = 76.64 * Math.Pow(F,0.6) / vitezaVantului;
                        xf  = 188 * Math.Pow(F, 0.4);
                    }


                } else {

                    if (stabilityClass == StabilityClassEnum.E) {

                        dHf = 6.54* Math.Pow(  (F*tempAtmosferei) / vitezaVantului,0.333);
                        xf  = 4.67* vitezaVantului * Math.sqrt(tempAtmosferei) ;

                    } else {

                        dHf = 5.43 * Math.Pow( (F*tempAtmosferei)/vitezaVantului,0.333);
                        xf  = 3.5 *vitezaVantului * Math.sqrt(tempAtmosferei);

                    }
                }

                if (Double.compare(dH,dHf)>0) {

                    dH  = dHf;
                    dHf = 0.0;
                }
        //    System.out.println("Algoritm efect termic");
            } else {
                  
                
            //    System.out.println("Algoritm efect de moment");
                //Algoritm efect moment
                dH = (3*diametrulCosului*vitezaGazului) / vitezaVantului;

                if ( stabilityClass.getValue()  > 4) {

                    if (stabilityClass == StabilityClassEnum.E) a = 1.24;
                    else          a = 1.129;

                    dHf = (a * Math.Pow(F, 0.667) * Math.sqrt(tempAtmosferei)) / Math.Pow(tempGazului*vitezaVantului,0.333);

                } else {

                    if ( Double.compare(dHf, dH) <0 ) dH = dHf;

                }

                
            }
      Ht = inaltimeaCosului+ dH;
      
   //   System.out.println(Ht+ " "+ u);
      }
      return Ht;
    }

}
