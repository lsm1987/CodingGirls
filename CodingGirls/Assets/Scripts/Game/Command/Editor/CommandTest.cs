using UnityEngine;
using NUnit.Framework;

namespace Game
{
    public class CommandTest
    {
        [Test]
        public void BackgroundParseTest()
        {
            string param = "Town";
            CommandError error;
            var cmd = new Command_Background();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("Town", cmd._Name);
        }

        [Test]
        public void BackGroundColorParseTest()
        {
            string param = "(255,0,255)";
            CommandError error;
            var cmd = new Command_BackgroundColor();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual(new Color(1.0f, 0.0f, 1.0f), cmd._Color);
        }

        [Test]
        public void ForegroundFadeInParseTest()
        {
            {
                string param = "0.5";
                CommandError error;
                var cmd = new Command_ForegroundFadeIn();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual(0.5f, cmd._Duration);
            }
            {
                string param = "";
                CommandError error;
                var cmd = new Command_ForegroundFadeIn();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual(0.0f, cmd._Duration);
            }
        }

        [Test]
        public void ForegroundFadeOutParseTest()
        {
            {
                string param = "0.5";
                CommandError error;
                var cmd = new Command_ForegroundFadeOut();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual(0.5f, cmd._Duration);
            }
            {
                string param = "";
                CommandError error;
                var cmd = new Command_ForegroundFadeOut();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual(0.0f, cmd._Duration);
            }
        }

        [Test]
        public void ForegroundCoverParseTest()
        {
            {
                string param = "left 0.5";
                CommandError error;
                var cmd = new Command_ForegroundCover();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual(true, cmd._ToLeft);
                Assert.AreEqual(0.5f, cmd._Duration);
            }
            {
                string param = "right";
                CommandError error;
                var cmd = new Command_ForegroundCover();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual(false, cmd._ToLeft);
                Assert.AreEqual(Define._foregroundCoverDefaultDuration, cmd._Duration);
            }
        }

        [Test]
        public void ForegroundSweepParseTest()
        {
            {
                string param = "left 0.5";
                CommandError error;
                var cmd = new Command_ForegroundSweep();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual(true, cmd._ToLeft);
                Assert.AreEqual(0.5f, cmd._Duration);
            }
            {
                string param = "right";
                CommandError error;
                var cmd = new Command_ForegroundSweep();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual(false, cmd._ToLeft);
                Assert.AreEqual(Define._foregroundCoverDefaultDuration, cmd._Duration);
            }
        }

        [Test]
        public void NameParseTest()
        {
            string param = "[나래]";
            CommandError error;
            var cmd = new Command_Name();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("[나래]", cmd._Name);
        }

        [Test]
        public void TextParseTest()
        {
            string param = @"[나래]\n동해물과 백두산이";
            CommandError error;
            var cmd = new Command_Text();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("[나래]\n동해물과 백두산이", cmd._Text);
        }

        [Test]
        public void HideTextParseTest()
        {
            string param = "";
            CommandError error;
            var cmd = new Command_HideText();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
        }

        [Test]
        public void LoadBGMParseTest()
        {
            string param = "Normal";
            CommandError error;
            var cmd = new Command_LoadBGM();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("Normal", cmd._Path);
        }

        [Test]
        public void BGMParseTest()
        {
            {
                string param = "Normal";
                CommandError error;
                var cmd = new Command_BGM();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("Normal", cmd._Path);
            }
            {
                string param = "";
                CommandError error;
                var cmd = new Command_BGM();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual(null, cmd._Path);
            }
        }

        [Test]
        public void SoundParseTest()
        {
            string param = "DooDoong";
            CommandError error;
            var cmd = new Command_Sound();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("DooDoong", cmd._Path);
        }

        [Test]
        public void WaitParseTest()
        {
            string param = "0.5";
            CommandError error;
            var cmd = new Command_Wait();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual(0.5f, cmd._Duration);
        }

        [Test]
        public void LoadModelParseTest()
        {
            string param = "Epsilon";
            CommandError error;
            var cmd = new Command_LoadModel();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("Epsilon", cmd._Name);
        }

        [Test]
        public void ModelParseTest()
        {
            {
                string param = "Epsilon idle true angry (0,-1,0.1) 0.5";
                CommandError error;
                var cmd = new Command_Model();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("Epsilon", cmd._Name);
                Assert.AreEqual("idle", cmd._MotionName);
                Assert.AreEqual(true, cmd._MotionLoop);
                Assert.AreEqual("angry", cmd._ExpressionName);
                Assert.AreEqual(new Vector3(0.0f, -1.0f, 0.1f), cmd._Position);
                Assert.AreEqual(0.5f, cmd._Duration);
            }
        }

        [Test]
        public void ModelHideParseTest()
        {
            {
                string param = "Epsilon 0.5";
                CommandError error;
                var cmd = new Command_ModelHide();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("Epsilon", cmd._Name);
                Assert.AreEqual(0.5f, cmd._Duration);
            }
            {
                string param = "Epsilon";
                CommandError error;
                var cmd = new Command_ModelHide();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("Epsilon", cmd._Name);
                Assert.AreEqual(0.0f, cmd._Duration);
            }
        }

        [Test]
        public void ModelRemoveParseTest()
        {
            {
                string param = "Epsilon";
                CommandError error;
                var cmd = new Command_ModelRemove();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("Epsilon", cmd._Name);
            }
        }

        [Test]
        public void MotionParseTest()
        {
            {
                string param = "Epsilon tilt true";
                CommandError error;
                var cmd = new Command_Motion();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("Epsilon", cmd._ModelName);
                Assert.AreEqual("tilt", cmd._MotionName);
                Assert.AreEqual(true, cmd._MotionLoop);
            }
            {
                string param = "Epsilon tilt";
                CommandError error;
                var cmd = new Command_Motion();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("Epsilon", cmd._ModelName);
                Assert.AreEqual("tilt", cmd._MotionName);
                Assert.AreEqual(false, cmd._MotionLoop);
            }
        }

        [Test]
        public void ExpressionParseTest()
        {
            {
                string param = "Epsilon angry";
                CommandError error;
                var cmd = new Command_Expression();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("Epsilon", cmd._ModelName);
                Assert.AreEqual("angry", cmd._ExpressionName);
            }
            {
                string param = "Epsilon";
                CommandError error;
                var cmd = new Command_Expression();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("Epsilon", cmd._ModelName);
                Assert.AreEqual("", cmd._ExpressionName);
            }
        }

        [Test]
        public void EyeBlinkParseTest()
        {
            string param = "Epsilon true";
            CommandError error;
            var cmd = new Command_EyeBlink();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("Epsilon", cmd._ModelName);
            Assert.AreEqual(true, cmd._Enable);
        }

        [Test]
        public void ModelPositionParseTest()
        {
            string param = "Epsilon (1.5,1.0,-1.0) 1.0";
            CommandError error;
            var cmd = new Command_ModelPosition();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("Epsilon", cmd._Name);
            Assert.AreEqual(new Vector3(1.5f, 1.0f, -1.0f), cmd._Position);
            Assert.AreEqual(1.0f, cmd._Duration);
        }

        [Test]
        public void LabelParseTest()
        {
            string param = "Choose_Restaurant";
            CommandError error;
            var cmd = new Command_Label();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("Choose_Restaurant", cmd._Name);
        }

        [Test]
        public void JumpParseTest()
        {
            string param = "Choose_Restaurant";
            CommandError error;
            var cmd = new Command_Jump();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("Choose_Restaurant", cmd._LabelName);
        }

        [Test]
        public void SelectParseTest()
        {
            string param = "";
            CommandError error;
            var cmd = new Command_Select();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreNotEqual(null, cmd);
        }

        [Test]
        public void SelectItemParseTest()
        {
            string param = "Choose_Restaurant 고급 레스토랑";
            CommandError error;
            var cmd = new Command_SelectItem();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("Choose_Restaurant", cmd._LabelName);
            Assert.AreEqual("고급 레스토랑", cmd._Text);
        }

        [Test]
        public void SelectEndParseTest()
        {
            string param = "";
            CommandError error;
            var cmd = new Command_SelectEnd();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreNotEqual(null, cmd);
        }

        [Test]
        public void TitleParseTest()
        {
            string param = "";
            CommandError error;
            var cmd = new Command_Title();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreNotEqual(null, cmd);
        }

        [Test]
        public void ScenarioParseTest()
        {
            {
                string param = "filename";
                CommandError error;
                var cmd = new Command_Scenario();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("filename", cmd._ScenarioName);
                Assert.AreEqual(string.Empty, cmd._LabelName);
            }
            {
                string param = "filename labelname";
                CommandError error;
                var cmd = new Command_Scenario();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("filename", cmd._ScenarioName);
                Assert.AreEqual("labelname", cmd._LabelName);
            }
        }

        [Test]
        public void SpriteParseTest()
        {
            string param = "ForeachCode (0,0.5) 2.2";
            CommandError error;
            var cmd = new Command_Sprite();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("ForeachCode", cmd._Name);
            Assert.AreEqual(new Vector2(0.0f, 0.5f), cmd._Position);
            Assert.AreEqual(2.2f, cmd._Scale);
        }

        [Test]
        public void RemoveSpriteParseTest()
        {
            {
                string param = "ForeachCode";
                CommandError error;
                var cmd = new Command_RemoveSprite();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("ForeachCode", cmd._Name);
            }
            {
                string param = "";
                CommandError error;
                var cmd = new Command_RemoveSprite();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual(string.Empty, cmd._Name);
            }
        }

        [Test]
        public void HideMenuParseTest()
        {
            string param = "";
            CommandError error;
            var cmd = new Command_HideMenu();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
        }

        [Test]
        public void ShowMenuParseTest()
        {
            string param = "";
            CommandError error;
            var cmd = new Command_ShowMenu();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
        }

        [Test]
        public void ShowLoadingParseTest()
        {
            string param = "";
            CommandError error;
            var cmd = new Command_ShowLoading();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
        }

        [Test]
        public void RemoveLoadingParseTest()
        {
            string param = "";
            CommandError error;
            var cmd = new Command_RemoveLoading();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
        }

        [Test]
        public void LinkParseTest()
        {
            string param = "Google (0,0.5) http://google.com";
            CommandError error;
            var cmd = new Command_Link();
            cmd.Parse(param, out error);

            Assert.AreEqual(null, error);
            Assert.AreEqual("Google", cmd._Name);
            Assert.AreEqual(new Vector2(0.0f, 0.5f), cmd._Position);
            Assert.AreEqual("http://google.com", cmd._Url);
        }

        [Test]
        public void RemoveLinkParseTest()
        {
            {
                string param = "Google";
                CommandError error;
                var cmd = new Command_RemoveLink();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("Google", cmd._Name);
            }
            {
                string param = "";
                CommandError error;
                var cmd = new Command_RemoveLink();
                cmd.Parse(param, out error);

                Assert.AreNotEqual(null, error);
            }
        }

        #region Presentation
        public class PresentationTest
        {
            [Test]
            public void PresentationParseTest()
            {
                string param = "";
                CommandError error;
                var cmd = new Command_Presentation();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
            }

            [Test]
            public void RemovePresentationParseTest()
            {
                string param = "";
                CommandError error;
                var cmd = new Command_RemovePresentation();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
            }

            [Test]
            public void PresentationLayoutParseTest()
            {
                {
                    string param = "";
                    CommandError error;
                    var cmd = new Command_PresentationLayout();
                    cmd.Parse(param, out error);

                    Assert.AreEqual(null, error);
                }
                {
                    string param = "TitleSlide";
                    CommandError error;
                    var cmd = new Command_PresentationLayout();
                    cmd.Parse(param, out error);

                    Assert.AreEqual(null, error);
                    Assert.AreEqual("TitleSlide", cmd._LayoutName);
                }
            }

            [Test]
            public void PresentationTextParseTest()
            {
                {
                    string param = "Title";
                    CommandError error;
                    var cmd = new Command_PresentationText();
                    cmd.Parse(param, out error);

                    Assert.AreEqual(null, error);
                    Assert.AreEqual("Title", cmd._TextName);
                    Assert.AreEqual(string.Empty, cmd._Text);
                }
                {
                    string param = "Title Awesome Text";
                    CommandError error;
                    var cmd = new Command_PresentationText();
                    cmd.Parse(param, out error);

                    Assert.AreEqual(null, error);
                    Assert.AreEqual("Title", cmd._TextName);
                    Assert.AreEqual("Awesome Text", cmd._Text);
                }
            }

            [Test]
            public void PresentationTextAppendParseTest()
            {
                string param = "Text \\nAwesome Text";
                CommandError error;
                var cmd = new Command_PresentationTextAppend();
                cmd.Parse(param, out error);

                Assert.AreEqual(null, error);
                Assert.AreEqual("Text", cmd._TextName);
                Assert.AreEqual("\nAwesome Text", cmd._Text);
            }
        }
        #endregion // Presentation
    }
}