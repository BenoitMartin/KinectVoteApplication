using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace KinectClient
{
    public partial class MainWindow : Window
    {
        private const float InferredZPositionClamp = 0.1f;
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
        private DrawingGroup drawingGroup;
        private DrawingImage imageSource;
        private KinectSensor kinectSensor = null;
        private CoordinateMapper coordinateMapper = null;
        private BodyFrameReader bodyFrameReader = null;
        private Body[] bodies = null;
        private List<Tuple<JointType, JointType>> bones;
        private int displayWidth;
        private int displayHeight;
        private Pen bodyColor;
        private Detecteur MoteurDetection = new Detecteur();
        private char DerniereLettre;
        private Boolean isInitiated = false;
        private Boolean VoteAvailable = true;
        private int numberVoter;
        public MainWindow()
        {
            this.kinectSensor = KinectSensor.GetDefault();
            this.coordinateMapper = this.kinectSensor.CoordinateMapper;
            FrameDescription frameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

            this.displayWidth = frameDescription.Width;
            this.displayHeight = frameDescription.Height;

            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            this.bones = new List<Tuple<JointType, JointType>>();

            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineBase));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.HandRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderLeft, JointType.ElbowLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowLeft, JointType.HandLeft));

            this.bodyColor = new Pen(Brushes.Black, 6);

            this.kinectSensor.Open();

            this.drawingGroup = new DrawingGroup();
            this.imageSource = new DrawingImage(this.drawingGroup);
            this.DataContext = this;
            this.InitializeComponent();
        }

        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.bodyFrameReader != null)
                this.bodyFrameReader.FrameArrived += this.Reader_FrameArrived;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.bodyFrameReader != null)
                this.bodyFrameReader.Dispose();

            if (this.kinectSensor != null)
                this.kinectSensor.Close();
        }

        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                        this.bodies = new Body[bodyFrame.BodyCount];
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    numberVoter = bodyFrame.BodyCount;
                    dataReceived = true;
                }
            }

            if (dataReceived)
            {
                using (DrawingContext dc = this.drawingGroup.Open())
                {
                    dc.DrawRectangle(Brushes.White, null, new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));

                    foreach (Body body in this.bodies)
                    {
                        Pen drawPen = this.bodyColor;

                        if (body.IsTracked)
                        {
                            IReadOnlyDictionary<JointType, Joint> joints = body.Joints;
                            Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();
                            foreach (JointType jointType in joints.Keys)
                            {
                                CameraSpacePoint position = joints[jointType].Position;
                                if (position.Z < 0)
                                    position.Z = InferredZPositionClamp;

                                DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                                jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
                            }

                            this.DrawBody(joints, jointPoints, dc, drawPen);
                        }
                    }
                }
            }
        }

        private Point TransformCameraPoint(CameraSpacePoint input)
        {
            double X = 320.0 + 320.0 * input.X;
            double Y = 240.0 - 240.0 * input.Y;
            return new Point(X, Y);
        }

        private void DrawBody(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            Point PosMainGauche = TransformCameraPoint(joints[JointType.HandLeft].Position);
            Point PosMainDroite = TransformCameraPoint(joints[JointType.HandRight].Position);
            Point PosCentreEpaule = TransformCameraPoint(joints[JointType.SpineShoulder].Position);
            Point PosCoudeGauche = TransformCameraPoint(joints[JointType.ElbowLeft].Position);
            Point PosCoudeDroit = TransformCameraPoint(joints[JointType.ElbowRight].Position);
            //Debug.WriteLine(string.Format("MG:{0} / MD:{1} / CE:{2} / CG:{3} / CD:{4}", PosMainGauche, PosMainDroite, PosCentreEpaule, PosCoudeGauche, PosCoudeDroit));
            Debug.WriteLine(string.Format("MG:{0}-{1} / MD:{2}-{3} / CE:{4}-{5} / CG:{6}-{7} / CD:{8}-{9}", Convert.ToInt16(PosMainGauche.X), Convert.ToInt16(PosMainGauche.Y), Convert.ToInt16(PosMainDroite.X), Convert.ToInt16(PosMainDroite.Y), Convert.ToInt16(PosCentreEpaule.X), Convert.ToInt16(PosCentreEpaule.Y), Convert.ToInt16(PosCoudeGauche.X), Convert.ToInt16(PosCoudeGauche.Y), Convert.ToInt16(PosCoudeDroit.X), Convert.ToInt16(PosCoudeDroit.Y)));


            if (numberVoter != 0 && VoteAvailable.Equals(true))
            {

                char Lettre = MoteurDetection.DetecterActionRealiseeCorrection(PosMainGauche, PosMainDroite, PosCentreEpaule, PosCoudeGauche, PosCoudeDroit);
                if (Lettre != DerniereLettre && (byte)Lettre != 0)
                {
                    DerniereLettre = Lettre;


                    if (Lettre.Equals('I'))
                    {
                        TexteReconnu.Text = "Vote initialisé, Préparez - vous à votez !";
                        isInitiated = true;
                    }
                    if (isInitiated)
                    {
                        ImageInit.Visibility = System.Windows.Visibility.Hidden;
                        ImageVote.Visibility = System.Windows.Visibility.Visible;
                        if (Lettre.Equals('O'))
                        {
                            TexteReconnu.Text = "Vous avez voté OUI ! Vous pouvez sortir.";
                            try
                            {
                                //string URI = "http://172.16.8.18:49229/Models/voteReceptor.asmx/Voter?Election=Alae&ValeurVote=Coucou&Personne= Alae";
                                string URI = "http://localhost:3527/VoteReceptor.asmx/Voter";
                                URI += "?Election=Femme&ValeurVote=OUI&Personne=Kinect";

                                using (WebClient wc = new WebClient())
                                {
                                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                                    string HtmlResult = wc.DownloadString(URI);
                                }


                            }


                            catch (WebException exp)
                            {

                                //throw exp;
                                MessageBox.Show(exp.Message);
                            }


                            VoteAvailable = false;
                            isInitiated = false;
                        }
                        else if (Lettre.Equals('N'))
                        {
                            TexteReconnu.Text = "Vous avez voté NON ! Vous pouvez sortir.";
                            try
                            {
                                //string URI = "http://172.16.8.18:49229/Models/voteReceptor.asmx/Voter?Election=Alae&ValeurVote=Coucou&Personne= Alae";
                                string URI = "http://localhost:3527/VoteReceptor.asmx/Voter";
                                URI += "?Election=Femme&ValeurVote=NON&Personne=Kinect";

                                using (WebClient wc = new WebClient())
                                {
                                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                                    string HtmlResult = wc.DownloadString(URI);
                                }


                            }


                            catch (WebException exp)
                            {

                                //throw exp;
                                MessageBox.Show(exp.Message);
                            }

                            VoteAvailable = false;
                            isInitiated = false;
                        }
                        else if (Lettre.Equals('E'))
                        {
                            TexteReconnu.Text = "En attente du vote";
                        }
                    }
                    else
                    {
                        ImageVote.Visibility = System.Windows.Visibility.Hidden;
                        ImageInit.Visibility = System.Windows.Visibility.Visible;
                        }
                }
            }
            else if (numberVoter == 0)
            {
                VoteAvailable = true;
                ImageVote.Visibility = System.Windows.Visibility.Hidden;
                ImageInit.Visibility = System.Windows.Visibility.Visible;

            }



            foreach (var bone in this.bones)
                this.DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
        }


        private void DrawBone(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];

            if (joint0.TrackingState == TrackingState.NotTracked ||
                joint1.TrackingState == TrackingState.NotTracked)
                return;

            drawingContext.DrawLine(drawingPen, jointPoints[jointType0], jointPoints[jointType1]);
        }
    }
}
