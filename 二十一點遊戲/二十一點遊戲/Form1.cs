using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 二十一點遊戲
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double bonusPlayer = 1000, bonusDealer = 1000;
        int win = 0, lose = 0, draw = 0;
        int bonus = 0;
        int hascardsPlayer, hascardsDealer;
        int[] deck = new int[52];
        PictureBox[] arrPicDealer = new PictureBox[5]; PictureBox[] arrPicPlayer = new PictureBox[5];
        int totalpointPlayer = 0, totalpointDealer = 0;
        int hasAcePlayer = 0, hasAceDealer = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            btnHit.Enabled = false; btnCheck.Enabled = false;
            lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
            lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
            arrPicDealer[0] = picDealer1; arrPicDealer[1] = picDealer2; arrPicDealer[2] = picDealer3;
            arrPicDealer[3] = picDealer4; arrPicDealer[4] = picDealer5;
            arrPicPlayer[0] = picPlayer1; arrPicPlayer[1] = picPlayer2; arrPicPlayer[2] = picPlayer3;
            arrPicPlayer[3] = picPlayer4; arrPicPlayer[4] = picPlayer5;
        }

        private void btnBonus10_Click(object sender, EventArgs e)
        {
            bonusPlayer -= 10;
            bonus += 10;
            lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
            lblBonus.Text = String.Format("本局賭注：{0}", bonus.ToString());

            if (bonusPlayer < 100)
            {
                btnBonus10.Enabled = false; btnBonus20.Enabled = false;
                btnBonus50.Enabled = false; btnBonus100.Enabled = false;
            }
        }

        private void btnBonus20_Click(object sender, EventArgs e)
        {
            bonusPlayer -= 20;
            bonus += 20;
            lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
            lblBonus.Text = String.Format("本局賭注：{0}", bonus.ToString());

            if (bonusPlayer < 100)
            {
                btnBonus10.Enabled = false; btnBonus20.Enabled = false;
                btnBonus50.Enabled = false; btnBonus100.Enabled = false;
            }
        }

        private void btnBonus50_Click(object sender, EventArgs e)
        {
            bonusPlayer -= 50;
            bonus += 50;
            lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
            lblBonus.Text = String.Format("本局賭注：{0}", bonus.ToString());

            if (bonusPlayer < 100)
            {
                btnBonus10.Enabled = false; btnBonus20.Enabled = false;
                btnBonus50.Enabled = false; btnBonus100.Enabled = false;
            }
        }

        private void btnBonus100_Click(object sender, EventArgs e)
        {
            bonusPlayer -= 100;
            bonus += 100;
            lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
            lblBonus.Text = String.Format("本局賭注：{0}", bonus.ToString());

            if (bonusPlayer < 100)
            {
                btnBonus10.Enabled = false; btnBonus20.Enabled = false;
                btnBonus50.Enabled = false; btnBonus100.Enabled = false;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (bonus == 0)
            {
                MessageBox.Show("請下注！");
                return;
            }

            Shuffle();
            btnStart.Enabled = false; btnBonus10.Enabled = false; btnBonus20.Enabled = false;
            btnBonus50.Enabled = false; btnBonus100.Enabled = false; btnHit.Enabled = true;
            btnCheck.Enabled = true;
            hascardsPlayer = 0; hascardsDealer = 0;
            totalpointDealer = 0; totalpointPlayer = 0;
            hasAcePlayer = 0; hasAceDealer = 0;

            for (int i = 2; i <= 4; i++)
            {
                arrPicDealer[i].Image = null;
                arrPicPlayer[i].Image = null;
            }

            for (int i = 0; i <= 1; i++)
            {
                int pointDealer = deck[hascardsDealer];

                hascardsDealer++;
                arrPicDealer[i].Image = Image.FromFile("img\\Poker" + pointDealer.ToString() + ".jpg");
                pointDealer = pointDealer % 13;

                if (pointDealer == 1)
                {
                    totalpointDealer += 11;
                    hasAceDealer++;
                }
                else if (pointDealer == 0)
                    totalpointDealer += 10;
                else
                    totalpointDealer += (pointDealer <= 10 ? pointDealer : 10);
                if (hasAceDealer == 2)
                {
                    totalpointDealer -= 10;
                    lblPointDealer.Text = String.Format("莊家的點數：{0}", totalpointDealer.ToString());
                }

                int pointPlayer = deck[hascardsDealer + hascardsPlayer + 1];

                hascardsPlayer++;
                arrPicPlayer[i].Image = Image.FromFile("img\\Poker" + pointPlayer.ToString() + ".jpg");
                pointPlayer = pointPlayer % 13;

                if (pointPlayer == 1)
                {
                    totalpointPlayer += 11;
                    hasAcePlayer++;
                }
                else if (pointPlayer == 0)
                    totalpointPlayer += 10;
                else
                    totalpointPlayer += (pointPlayer <= 10 ? pointPlayer : 10);
                if (hasAcePlayer == 2)
                {
                    totalpointPlayer -= 10;
                    lblPointPlayer.Text = String.Format("閒家的點數：{0}", totalpointPlayer.ToString());
                }
            }
            if (hasAceDealer == 2)
                totalpointDealer -= 10;
            if (hasAcePlayer == 2)
                totalpointPlayer -= 10;
            if (totalpointPlayer == 21)
                btnHit.Enabled = false;
            lblPointDealer.Text = String.Format("莊家的點數：{0}", totalpointDealer.ToString());
            lblPointPlayer.Text = String.Format("閒家的點數：{0}", totalpointPlayer.ToString());
        }

        private void btnHit_Click(object sender, EventArgs e)
        {
            int pointPlayer = deck[hascardsDealer + hascardsPlayer + 1];

            switch (hascardsPlayer)
            {
                case 2:
                    hascardsPlayer++;
                    arrPicPlayer[2].Image = Image.FromFile("img\\Poker" + pointPlayer.ToString() + ".jpg");
                    pointPlayer = pointPlayer % 13;
                    if (pointPlayer == 1 && hasAcePlayer == 0 && totalpointPlayer <= 10)
                    {
                        totalpointPlayer += 11;
                        hasAcePlayer++;
                    }
                    else if (pointPlayer == 0)
                        totalpointPlayer += 10;
                    else
                        totalpointPlayer += (pointPlayer <= 10 ? pointPlayer : 10);
                    lblPointPlayer.Text = String.Format("閒家的點數：{0}", totalpointPlayer.ToString());
                    if (totalpointPlayer > 21)
                    {
                        if (hasAcePlayer >= 1)
                        {
                            totalpointPlayer -= 10;
                            lblPointPlayer.Text = String.Format("閒家的點數：{0}", totalpointPlayer.ToString());
                            if (totalpointPlayer == 21)
                            {
                                btnHit.Enabled = false;
                            }
                            else if (totalpointPlayer > 21)
                            {
                                btnHit.Enabled = false; btnCheck.Enabled = false; btnRestart.Visible = true;
                                bonusDealer += bonus;
                                lose++;
                                lblMsg.Text = String.Format("閒家爆煲了，你失去{0}的賭金！", bonus.ToString());
                                lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                                lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                                lblLose.Text = String.Format("敗：{0}", lose.ToString());
                                lblBonus.Text = "本局賭注：";
                            }
                        }
                        else
                        {
                            btnHit.Enabled = false; btnCheck.Enabled = false; btnRestart.Visible = true;
                            bonusDealer += bonus;
                            lose++;
                            lblMsg.Text = String.Format("閒家爆煲了，你失去{0}的賭金！", bonus.ToString());
                            lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                            lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                            lblLose.Text = String.Format("敗：{0}", lose.ToString());
                            lblBonus.Text = "本局賭注：";
                        }
                    }
                    else if (totalpointPlayer == 21)
                    {
                        btnHit.Enabled = false;
                    }
                    break;
                case 3:
                    hascardsPlayer++;
                    arrPicPlayer[3].Image = Image.FromFile("img\\Poker" + pointPlayer.ToString() + ".jpg");
                    pointPlayer = pointPlayer % 13;
                    if (pointPlayer == 1 && hasAcePlayer == 0 && totalpointPlayer <= 10)
                    {
                        totalpointPlayer += 11;
                        hasAcePlayer++;
                    }
                    else if (pointPlayer == 0)
                        totalpointPlayer += 10;
                    else
                        totalpointPlayer += (pointPlayer <= 10 ? pointPlayer : 10);
                    lblPointPlayer.Text = String.Format("閒家的點數：{0}", totalpointPlayer.ToString());
                    if (totalpointPlayer > 21)
                    {
                        if (hasAcePlayer >= 1)
                        {
                            totalpointPlayer -= 10;
                            lblPointPlayer.Text = String.Format("閒家的點數：{0}", totalpointPlayer.ToString());
                            if (totalpointPlayer == 21)
                            {
                                btnHit.Enabled = false;
                            }
                            else if (totalpointPlayer > 21)
                            {
                                btnHit.Enabled = false; btnCheck.Enabled = false; btnRestart.Visible = true;
                                bonusDealer += bonus;
                                lose++;
                                lblMsg.Text = String.Format("閒家爆煲了，你失去{0}的賭金！", bonus.ToString());
                                lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                                lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                                lblLose.Text = String.Format("敗：{0}", lose.ToString());
                                lblBonus.Text = "本局賭注：";
                            }
                        }
                        else
                        {
                            btnHit.Enabled = false; btnCheck.Enabled = false; btnRestart.Visible = true;
                            bonusDealer += bonus;
                            lose++;
                            lblMsg.Text = String.Format("閒家爆煲了，你失去{0}的賭金！", bonus.ToString());
                            lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                            lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                            lblLose.Text = String.Format("敗：{0}", lose.ToString());
                            lblBonus.Text = "本局賭注：";
                        }
                    }
                    else if (totalpointPlayer == 21)
                    {
                        btnHit.Enabled = false;
                    }
                    break;
                case 4:
                    hascardsPlayer++;
                    btnHit.Enabled = false;
                    arrPicPlayer[4].Image = Image.FromFile("img\\Poker" + pointPlayer.ToString() + ".jpg");
                    pointPlayer = pointPlayer % 13;
                    if (pointPlayer == 1 && hasAcePlayer == 0 && totalpointPlayer <= 10)
                    {
                        totalpointPlayer += 11;
                        hasAcePlayer++;
                    }
                    else if (pointPlayer == 0)
                        totalpointPlayer += 10;
                    else
                        totalpointPlayer += (pointPlayer <= 10 ? pointPlayer : 10);
                    lblPointPlayer.Text = String.Format("閒家的點數：{0}", totalpointPlayer.ToString());
                    if (totalpointPlayer > 21)
                    {
                        if (hasAcePlayer >= 1)
                        {
                            totalpointPlayer -= 10;
                            lblPointPlayer.Text = String.Format("閒家的點數：{0}", totalpointPlayer.ToString());
                            if (totalpointPlayer < 21)
                            {
                                btnHit.Enabled = false; btnCheck.Enabled = false; btnRestart.Visible = true;
                                bonusPlayer += bonus * 2 + bonus;
                                bonusDealer -= bonus * 2;
                                win++;
                                lblMsg.Text = String.Format("閒家過五關，你獲得{0}的賭金！", (bonus * 2).ToString());
                                lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                                lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                                lblWin.Text = String.Format("勝：{0}", win.ToString());
                                lblBonus.Text = "本局賭注：";
                            }
                            else
                            {
                                btnHit.Enabled = false; btnCheck.Enabled = false; btnRestart.Visible = true;
                                bonusDealer += bonus;
                                lose++;
                                lblMsg.Text = String.Format("閒家爆煲了，你失去{0}的賭金！", bonus.ToString());
                                lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                                lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                                lblLose.Text = String.Format("敗：{0}", lose.ToString());
                                lblBonus.Text = "本局賭注：";
                            }
                        }
                        else
                        {
                            btnHit.Enabled = false; btnCheck.Enabled = false; btnRestart.Visible = true;
                            bonusDealer += bonus;
                            lose++;
                            lblMsg.Text = String.Format("閒家爆煲了，你失去{0}的賭金！", bonus.ToString());
                            lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                            lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                            lblLose.Text = String.Format("敗：{0}", lose.ToString());
                            lblBonus.Text = "本局賭注：";
                        }
                    }
                    else
                    {
                        btnHit.Enabled = false; btnCheck.Enabled = false; btnRestart.Visible = true;
                        bonusPlayer += bonus * 2 + bonus;
                        bonusDealer -= bonus * 2;
                        win += 1;
                        lblMsg.Text = String.Format("閒家過五關，你獲得{0}的賭金！", (bonus * 2).ToString());
                        lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                        lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                        lblWin.Text = String.Format("勝：{0}", win.ToString());
                        lblBonus.Text = "本局賭注：";
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            btnHit.Enabled = false;

            while (totalpointDealer < totalpointPlayer)
            {
                int pointDealer = deck[hascardsPlayer + hascardsDealer + 1];
                switch (hascardsDealer)
                {
                    case 2:
                        hascardsDealer++;
                        arrPicDealer[2].Image = Image.FromFile("img\\Poker" + pointDealer.ToString() + ".jpg");
                        pointDealer = pointDealer % 13;

                        if (pointDealer == 1 && hasAceDealer == 0 && totalpointDealer <= 10)
                        {
                            totalpointDealer += 11;
                            hasAceDealer++;
                        }
                        else if (pointDealer == 0)
                            totalpointDealer += 10;
                        else
                            totalpointDealer += (pointDealer <= 10 ? pointDealer : 10);
                        break;
                    case 3:
                        hascardsDealer++;
                        arrPicDealer[3].Image = Image.FromFile("img\\Poker" + pointDealer.ToString() + ".jpg");
                        pointDealer = pointDealer % 13;

                        if (pointDealer == 1 && hasAceDealer == 0 && totalpointDealer <= 10)
                        {
                            totalpointDealer += 11;
                            hasAceDealer++;
                        }
                        else if (pointDealer == 0)
                            totalpointDealer += 10;
                        else
                            totalpointDealer += (pointDealer <= 10 ? pointDealer : 10);
                        break;
                    case 4:
                        hascardsDealer++;
                        arrPicDealer[4].Image = Image.FromFile("img\\Poker" + pointDealer.ToString() + ".jpg");
                        pointDealer = pointDealer % 13;

                        if (pointDealer == 1 && hasAceDealer == 0 && totalpointDealer <= 10)
                        {
                            totalpointDealer += 11;
                            hasAceDealer++;
                        }
                        else if (pointDealer == 0)
                            totalpointDealer += 10;
                        else
                            totalpointDealer += (pointDealer <= 10 ? pointDealer : 10);
                        break;
                    default:
                        break;
                }
            }

            lblPointDealer.Text = String.Format("莊家的點數：{0}", totalpointDealer.ToString());

            if (totalpointDealer > 21)
            {
                btnCheck.Enabled = false; btnRestart.Visible = true;
                if (hasAceDealer >= 1)
                {
                    totalpointDealer -= 10;
                    lblPointDealer.Text = String.Format("莊家的點數：{0}", totalpointDealer.ToString());
                    if(totalpointDealer < totalpointPlayer)
                    {
                        btnCheck_Click(sender, e);
                    }
                    else if (totalpointDealer > 21)
                    {
                        btnCheck.Enabled = false; btnRestart.Visible = true;
                        bonusPlayer += bonus * 2;
                        bonusDealer -= bonus;
                        win++;
                        lblMsg.Text = String.Format("莊家爆煲，你獲得{0}的賭金！", bonus);
                        lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                        lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                        lblWin.Text = String.Format("勝：{0}", win.ToString());
                        lblBonus.Text = "本局賭注：";
                    }
                    else if (totalpointPlayer > totalpointDealer || (totalpointPlayer == 21 && totalpointDealer == 21 && hascardsPlayer == 2 && hascardsDealer != 2))
                    {
                        btnCheck.Enabled = false; btnRestart.Visible = true;
                        bonusPlayer += bonus * 2;
                        bonusDealer -= bonus;
                        win++;
                        lblMsg.Text = String.Format("閒家獲勝，你獲得{0}的賭金！", bonus);
                        lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                        lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                        lblWin.Text = String.Format("勝：{0}", win.ToString());
                        lblBonus.Text = "本局賭注：";
                    }
                    else if ((totalpointPlayer > totalpointDealer && hascardsDealer == 5) || totalpointDealer > totalpointPlayer || (totalpointDealer == totalpointPlayer && hascardsDealer <= 4))
                    {
                        btnCheck.Enabled = false; btnRestart.Visible = true;
                        bonusDealer += bonus;
                        lose++;
                        lblMsg.Text = String.Format("莊家獲勝，你失去{0}的賭金！", bonus.ToString());
                        lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                        lblLose.Text = String.Format("敗：{0}", lose.ToString());
                        lblBonus.Text = "本局賭注：";
                    }
                    else
                    {
                        btnCheck.Enabled = false; btnRestart.Visible = true;
                        bonusPlayer += bonus;
                        draw++;
                        lblMsg.Text = String.Format("雙方平手，你拿回{0}的賭金！", bonus.ToString());
                        lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                        lblDraw.Text = String.Format("和：{0}", draw.ToString());
                        lblBonus.Text = "本局賭注：";
                    }
                }
                else
                {
                    btnCheck.Enabled = false; btnRestart.Visible = true;
                    bonusPlayer += bonus * 2;
                    bonusDealer -= bonus;
                    win++;
                    lblMsg.Text = String.Format("莊家爆煲，你獲得{0}的賭金！", bonus);
                    lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                    lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                    lblWin.Text = String.Format("勝：{0}", win.ToString());
                    lblBonus.Text = "本局賭注：";
                }
            }
            else if (totalpointPlayer > totalpointDealer || (totalpointPlayer == 21 && totalpointDealer == 21 && hascardsPlayer == 2 && hascardsDealer != 2))
            {
                btnCheck.Enabled = false; btnRestart.Visible = true;
                bonusPlayer += bonus * 2;
                bonusDealer -= bonus;
                win++;
                lblMsg.Text = String.Format("閒家獲勝，你獲得{0}的賭金！", bonus);
                lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                lblBonusPlayer.Text = String.Format("閒家的積分：{0}", bonusPlayer.ToString());
                lblWin.Text = String.Format("勝：{0}", win.ToString());
                lblBonus.Text = "本局賭注：";
            }
            else if ((totalpointPlayer > totalpointDealer && hascardsDealer == 5) || totalpointDealer > totalpointPlayer || (totalpointDealer == totalpointPlayer && hascardsDealer <= 4))
            {
                btnCheck.Enabled = false; btnRestart.Visible = true;
                bonusDealer += bonus;
                lose++;
                lblMsg.Text = String.Format("莊家獲勝，你失去{0}的賭金！", bonus.ToString());
                lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                lblLose.Text = String.Format("敗：{0}", lose.ToString());
                lblBonus.Text = "本局賭注：";
            }
            else
            {
                btnCheck.Enabled = false; btnRestart.Visible = true;
                bonusPlayer += bonus;
                draw++;
                lblMsg.Text = String.Format("雙方平手，你拿回{0}的賭金！", bonus.ToString());
                lblBonusDealer.Text = String.Format("莊家的積分：{0}", bonusDealer.ToString());
                lblDraw.Text = String.Format("和：{0}", draw.ToString());
                lblBonus.Text = "本局賭注：";
            }
            if(totalpointPlayer == 0)
            {
                MessageBox.Show("你已經沒有籌碼了！");
                Application.Restart();
            }
            if(totalpointDealer == 0)
            {
                MessageBox.Show("你打敗莊家了！");
                Application.Restart();
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true; btnBonus10.Enabled = true;btnBonus20.Enabled = true;
            btnBonus50.Enabled = true;btnBonus100.Enabled = true; btnRestart.Visible = false;

            bonus = 0;
            totalpointDealer = 0; totalpointPlayer = 0;

            lblMsg.Text = "";
            lblPointDealer.Text = String.Format("莊家的點數：{0}", totalpointPlayer.ToString());
            lblPointPlayer.Text = String.Format("閒家的點數：{0}", totalpointPlayer.ToString());
            lblBonus.Text = String.Format("本局賭注：{0}", bonus.ToString());

            for (int i = 0; i <= 4; i++)
            {
                arrPicDealer[i].Image = null;
                arrPicPlayer[i].Image = null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Shuffle()
        {
            int prev, next;

            Random rnd = new Random();

            for (int i = 0; i <= 51; i++)
                deck[i] = i + 1;

            for (int i = 1; i <= 1000; i++)
            {
                prev = rnd.Next(52);
                next = rnd.Next(52);
                int tmp = deck[prev];
                deck[prev] = deck[next];
                deck[next] = tmp;
            }
        }
    }
}