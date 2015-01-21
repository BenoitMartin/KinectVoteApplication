using KinectClient;
using System;
using System.Diagnostics;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestKinect
{
    [TestClass]
    public class KinectVotingTest
    {
        [TestMethod]
        public void TestInitialisationCorrecte()
        {
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(
                //89 174 584 166 147 157 199 169 489 164

                //int XMainGauche, 
            89,
                //int YMainGauche,
            174,
                //int XMainDroite, 
            584,
                //int YMainDroite,
            166,
                //int XCentreEpaule,
           147,
                //int YCentreEpaule,
            157,
                //int XCoudeGauche,
            199,
                //int YCoudeGauche,
            169,
                //int XCoudeDroit, 
            489,
                //int YCoudeDroit
            164
            );

            Assert.AreEqual(ValeurVote, 'I');

        }
        [TestMethod]
        public void TestInitialisationLimiteBassePositive()
        { // 72 196 537 217 299 170 168 191 440 207

            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(
               72, 196, 537, 217, 299, 170, 168, 191, 440, 207
             );

            Assert.AreEqual(ValeurVote, 'I');

        }
        [TestMethod]
        public void TestInitialisationLimiteHautePositive()
        { // 82 129 536 142 300 167 178 166 441 175
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

              82, 129, 536, 142, 300, 167, 178, 166, 441, 175
            );

            Assert.AreEqual(ValeurVote, 'I');
        }
        [TestMethod]
        public void TestInitialisationLimiteBasseNégative()
        { // 90 250 484 279 287 171 167 218 404 236
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(
              90, 250, 484, 279, 287, 171, 167, 218, 404, 236
            );

            Assert.AreNotEqual(ValeurVote, 'I');
        }

        [TestMethod]
        public void TestInitialisationLimiteHauteNégative()
        { // 121 68 492 86 294 165 177 135 432 152
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(
                121, 68, 492, 86, 294, 165, 177, 135, 432, 152
            );

            Assert.AreNotEqual(ValeurVote, 'I');
        }

        [TestMethod]
        public void TestInitialisationIncorrecte()
        { // 200,275,250,290,200,150,115,233,266,237
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

               200, 275, 250, 290, 200, 150, 115, 233, 266, 237
            );

            Assert.AreNotEqual(ValeurVote, 'I');
        }





        // ****** Main Droite ********

        // Vote Oui
        [TestMethod]
        public void TestVoteOuiLimiteBassePositiveMainDroite()
        { // 260,300,427,166,314,168,244,246,410,228
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(
               260, 300, 427, 166, 314, 168, 244, 246, 410, 228
            );

            Assert.AreEqual(ValeurVote, 'O');
        }
        [TestMethod]
        public void TestVoteOuiLimiteHautePositiveMainDroite()
        {
            /* int XMainGauche, int YMainGauche,int XMainDroite, int YMainDroite,int XCentreEpaule, int YCentreEpaule,
             * int XCoudeGauche, int YCoudeGauche,
            int XCoudeDroit, int YCoudeDroit) */
            // 187,300,330,165,218,158,153,234,284,246

            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

               187, 300, 330, 158, 218, 165, 153, 234, 284, 246
            );

            Assert.AreEqual(ValeurVote, 'O');
        }
        [TestMethod]
        public void TestVoteOuiLimiteBasseNégativeMainDroite()
        { // 146,290,308,179,224,164,140,240,307,236
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

               146, 290, 308, 179, 224, 164, 140, 240, 307, 236
            );
            Console.WriteLine(ValeurVote);
            Assert.AreNotEqual(ValeurVote, 'O');
        }
        [TestMethod]
        public void TestVoteOuiLimiteHauteNégativeMainDroite()
        { // 161,282,312,65,190,160,105,235,315,140
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

              161, 282, 312, 65, 190, 160, 105, 235, 315, 140
            );

            Assert.AreNotEqual(ValeurVote, 'O');
        }
        [TestMethod]
        public void TestVoteOuiCorrecteMainDroite()
        { // 165,265,300,101,177,160,104,237,300,180
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(
                //187, 300, 330, 165, 218, 158, 153, 234, 284, 246 
              165, 265, 300, 101, 177, 160, 104, 237, 300, 180
            );

            Assert.AreEqual(ValeurVote, 'O');
        }

        [TestMethod]
        public void TestVoteOuiIncorrecteMainDroite()
        { // comme incorrecte init
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

               200, 275, 250, 290, 200, 150, 115, 233, 266, 237
            );

            Assert.AreNotEqual(ValeurVote, 'O');
        }





        // Vote Non

        [TestMethod]
        public void TestVoteNonIncorrecteMainDroite()
        { // Comme les autres
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

               200, 275, 250, 290, 200, 150, 115, 233, 266, 237
            );

            Assert.AreNotEqual(ValeurVote, 'N');
        }

        [TestMethod]
        public void TestVoteNonCorrecteMainDroite()
        { // 105,305,300,250,167,164,90,242,293,195
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

               105, 305, 300, 250, 167, 164, 90, 242, 293, 195
            );

            Assert.AreNotEqual(ValeurVote, 'I');
        }


        [TestMethod]
        public void TestVoteNonLimiteBassePositiveMainDroite()
        { // 150,277,290,275,180,165,93,238,303,211
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

              150, 277, 290, 275, 180, 165, 93, 238, 303, 211
            );

            Assert.AreNotEqual(ValeurVote, 'I');
        }
        [TestMethod]
        public void TestVoteNonLimiteHautePositiveMainDroite()
        { // 146,288,309,222,177,165,93,243,308,174
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

              146, 288, 309, 222, 177, 165, 93, 243, 308, 174
            );

            Assert.AreNotEqual(ValeurVote, 'I');
        }
        [TestMethod]
        public void TestVoteNonLimiteBasseNégativeMainDroite()
        { // 139,280,296,287,182,165,87,233,300,216
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

              139, 280, 296, 287, 182, 165, 87, 233, 300, 216
            );

            Assert.AreNotEqual(ValeurVote, 'N');
        }
        [TestMethod]
        public void TestVoteNonLimiteHauteNégativeMainDroite()
        { // 138,284,313,181,172,163,80,235,298,185
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

              105, 305, 300, 250, 167, 164, 90, 242, 293, 195
            );

            Assert.AreEqual(ValeurVote, 'N');
        }




        // ****** Main Gauche ********

        // Vote Oui


        [TestMethod]
        public void TestVoteOuiCorrecteMainGauche()
        { // 65,118,246,307,175,162,55,189,246,242
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

                65, 118, 246, 307, 175, 162, 55, 189, 246, 242
            );

            Assert.AreEqual(ValeurVote, 'O');
        }

        [TestMethod]
        public void TestVoteOuiIncorrecteMainGauche()
        { // comme les autres
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

               200, 275, 250, 290, 200, 150, 115, 233, 266, 237
            );

            Assert.AreNotEqual(ValeurVote, 'O');
        }

        [TestMethod]
        public void TestVoteOuiLimiteBassePositiveMainGauche()
        { //240,162,404,295,357,166,258,222,426,237
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(
                240, 162, 404, 295, 357, 166, 258, 222, 426, 237
            );

            Assert.AreEqual(ValeurVote, 'O');
        }
        [TestMethod]
        public void TestVoteOuiLimiteHautePositiveMainGauche()
        { // 195,107,383,303,346,171,207,176,409,248
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(
             195, 107, 383, 303, 346, 171, 207, 176, 409, 248
            );

            Assert.AreEqual(ValeurVote, 'O');
        }
        [TestMethod]
        public void TestVoteOuiLimiteBasseNégativeMainGauche()
        { // 118,204,250,313,173,163,113,240,247,240
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

               118, 204, 250, 313, 173, 163, 113, 240, 247, 240
            );

            Assert.AreNotEqual(ValeurVote, 'O');
        }
        [TestMethod]
        public void TestVoteOuiLimiteHauteNégativeMainGauche()
        { // 88,50,251,313,185,160,80,126,256,245
            /* int XMainGauche, int YMainGauche,int XMainDroite, int YMainDroite,int XCentreEpaule, int YCentreEpaule,
           * int XCoudeGauche, int YCoudeGauche,
          int XCoudeDroit, int YCoudeDroit) */
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

               88, 50, 251, 313, 185, 160, 80, 126, 256, 245
            );

            Assert.AreNotEqual(ValeurVote, 'O');
        }






        // Vote Non

        [TestMethod]
        public void TestVoteNonIncorrecteMainGauche()
        { // comme les autres
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

              200, 275, 250, 290, 200, 150, 115, 233, 266, 237
            );

            Assert.AreNotEqual(ValeurVote, 'N');
        }

        [TestMethod]
        public void TestVoteNonCorrecteMainGauche()
        { // 57,236,236,313,172,162,52,177,238,245
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

              57, 236, 236, 313, 172, 162, 52, 177, 238, 245
            );

            Assert.AreEqual(ValeurVote, 'N');
        }

        [TestMethod]
        public void TestVoteNonLimiteBassePositiveMainGauche()
        { // 80,250,237,313,173,163,64,193,236,245
            //105, 274, 252, 309, 185, 162, 92, 208, 253, 244
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

              105, 274, 252, 309, 185, 162, 92, 208, 253, 244
            );

            Assert.AreEqual(ValeurVote, 'N');
        }
        [TestMethod]
        public void TestVoteNonLimiteHautePositiveMainGauche()
        { // 72,206,236,316,174,162,68,161,239,247
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

              72, 206, 236, 316, 174, 162, 68, 161, 239, 247
            );

            Assert.AreEqual(ValeurVote, 'N');
        }
        [TestMethod]
        public void TestVoteNonLimiteBasseNégativeMainGauche()
        { // 195,294,383,305,308,168,207,225,389,250
            // 105, 274, 252, 309, 185, 162, 92, 208, 253, 244
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(
             195, 294, 383, 305, 308, 168, 207, 225, 389, 250
            );

            Assert.AreNotEqual(ValeurVote, 'N');
        }
        [TestMethod]
        public void TestVoteNonLimiteHauteNégativeMainGauche()
        { // 76,155,243,313,180,162,66,152,245,247
            Detecteur Detecteur = new Detecteur();
            Char ValeurVote = new Char();
            ValeurVote = Detecteur.DetecterActionRealisee(

               76, 155, 243, 313, 180, 162, 66, 152, 245, 247
            );

            Assert.AreNotEqual(ValeurVote, 'N');
        }


    }

}