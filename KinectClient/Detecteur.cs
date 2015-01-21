using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace KinectClient
{
    public class Detecteur
    {
        public char DetecterActionRealisee(
            int XMainGauche, int YMainGauche,
            int XMainDroite, int YMainDroite,
            int XCentreEpaule, int YCentreEpaule,
            int XCoudeGauche, int YCoudeGauche,
            int XCoudeDroit, int YCoudeDroit)
        {
            //return DetecterActionRealisee_Poule2(XMainGauche, YMainGauche, XMainDroite, YMainDroite, XCentreEpaule, YCentreEpaule, XCoudeGauche, YCoudeGauche, XCoudeDroit, YCoudeDroit);

            // VERSION DE CORRECTION

            return DetecterActionRealiseeCorrection(
                new Point(XMainGauche, YMainGauche),
                new Point(XMainDroite, YMainDroite),
                new Point(XCentreEpaule, YCentreEpaule),
                new Point(XCoudeGauche, YCoudeGauche),
                new Point(XCoudeDroit, YCoudeDroit));
        }



        public char DetecterActionRealiseeCorrection(Point PosMainGauche, Point PosMainDroite, Point PosCentreEpaule, Point PosCoudeGauche, Point PosCoudeDroit)
        {
            //Debug.Write(string.Format("DX1 : {0} / DX2 : {1} / DY1 : {2} / DY2 : {3}", DX1, DX2, DY1, DY2));
            //Console.WriteLine(APeuPresEgal(PosMainDroite.X, PosCoudeDroit.X));
            //Console.WriteLine((PosCoudeDroit.Y > PosCentreEpaule.Y));
            //Console.WriteLine(PosMainDroite.Y < PosCentreEpaule.Y);
            /*
                 // Vote Positif Main Droite
            else if ((APeuPresEgal(PosMainDroite.X, PosCoudeDroit.X))
                      && (PosCoudeDroit.Y > PosCentreEpaule.Y)
                      && (PosMainDroite.Y < PosCentreEpaule.Y)
 
             */


            // Posture d'initialisation - un T
            if (APeuPresEgal(PosMainDroite.Y, PosCoudeDroit.Y)
              && APeuPresEgal(PosCoudeDroit.Y, PosCentreEpaule.Y)
              && APeuPresEgal(PosMainGauche.Y, PosCoudeGauche.Y)
              && APeuPresEgal(PosCoudeGauche.Y, PosCentreEpaule.Y)
              )
            {
                return 'I';
            }

          // Vote Positif Main Droite
            else if ((APeuPresEgal(PosMainDroite.X, PosCoudeDroit.X))
                      && (PosCoudeDroit.Y > PosCentreEpaule.Y)
                      && (PosMainDroite.Y < PosCentreEpaule.Y)
                  )
            {
                return 'O';
            }


            // Vote Négatif Main Droite
            else if (APeuPresEgal(PosMainDroite.X, PosCoudeDroit.X)
                 && APeuPresEgal(PosCoudeDroit.Y, PosCentreEpaule.Y)
                 && PosMainDroite.Y > PosCentreEpaule.Y
             )
            {
                return 'N';
            }


            // Vote Positif Main Gauche
            else if (APeuPresEgal(PosMainGauche.X, PosCoudeGauche.X)
                && ((PosCoudeGauche.Y > PosCentreEpaule.Y))
                 && PosMainGauche.Y < PosCentreEpaule.Y
             )
            {
                return 'O';
            }


            // Vote Négatif Main Gauche
            else if (APeuPresEgal(PosMainGauche.X, PosCoudeGauche.X)
                     && APeuPresEgal(PosCoudeGauche.Y, PosCentreEpaule.Y)
                     && PosMainGauche.Y > PosCentreEpaule.Y
                 )
            {
                return 'N';
            }
            else
            {
                //Debug.WriteLine(" => PAS DE DETECTION");
                return (char)7;
            }
        }

        private double Erreur = 50;

        private bool APeuPresEgal(double Nombre1, double Nombre2)
        {
            return EstEntre(Nombre1, Nombre2 - Erreur, Nombre2 + Erreur)
                || EstEntre(Nombre2, Nombre1 - Erreur, Nombre1 + Erreur);
        }

        private bool EstEntre(double Nombre, double BorneInf, double BorneSup)
        {
            return Nombre >= BorneInf && Nombre <= BorneSup;
        }
    }
}
