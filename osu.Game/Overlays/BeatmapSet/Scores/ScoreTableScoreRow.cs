// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Online.Leaderboards;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using osu.Game.Scoring;
using osu.Game.Users;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Overlays.BeatmapSet.Scores
{
    public class ScoreTableScoreRow : ScoreTableRow
    {
        private readonly int index;
        private readonly ScoreInfo score;

        public ScoreTableScoreRow(int index, ScoreInfo score)
        {
            this.index = index;
            this.score = score;
        }

        protected override Drawable CreateIndexCell() => new OsuSpriteText
        {
            Text = $"#{index + 1}",
            Font = OsuFont.GetFont(size: TEXT_SIZE, weight: FontWeight.Bold)
        };

        protected override Drawable CreateRankCell() => new DrawableRank(score.Rank)
        {
            Size = new Vector2(30, 20),
        };

        protected override Drawable CreateScoreCell() => new OsuSpriteText
        {
            Text = $@"{score.TotalScore:N0}",
            Font = OsuFont.GetFont(size: TEXT_SIZE, weight: index == 0 ? FontWeight.Bold : FontWeight.Medium)
        };

        protected override Drawable CreateAccuracyCell() => new OsuSpriteText
        {
            Text = $@"{score.Accuracy:P2}",
            Font = OsuFont.GetFont(size: TEXT_SIZE),
            Colour = score.Accuracy == 1 ? Color4.GreenYellow : Color4.White
        };

        protected override Drawable CreatePlayerCell() => new FillFlowContainer
        {
            AutoSizeAxes = Axes.Both,
            Direction = FillDirection.Horizontal,
            Spacing = new Vector2(5, 0),
            Children = new Drawable[]
            {
                new DrawableFlag(score.User.Country) { Size = new Vector2(20, 13) },
                new ClickableScoreUsername { User = score.User }
            }
        };

        protected override IEnumerable<Drawable> CreateStatisticsCells()
        {
            yield return new OsuSpriteText
            {
                Text = $@"{score.MaxCombo:N0}x",
                Font = OsuFont.GetFont(size: TEXT_SIZE)
            };

            yield return new OsuSpriteText
            {
                Text = $"{score.Statistics[HitResult.Great]}",
                Font = OsuFont.GetFont(size: TEXT_SIZE),
                Colour = score.Statistics[HitResult.Great] == 0 ? Color4.Gray : Color4.White
            };

            yield return new OsuSpriteText
            {
                Text = $"{score.Statistics[HitResult.Good]}",
                Font = OsuFont.GetFont(size: TEXT_SIZE),
                Colour = score.Statistics[HitResult.Good] == 0 ? Color4.Gray : Color4.White
            };

            yield return new OsuSpriteText
            {
                Text = $"{score.Statistics[HitResult.Meh]}",
                Font = OsuFont.GetFont(size: TEXT_SIZE),
                Colour = score.Statistics[HitResult.Meh] == 0 ? Color4.Gray : Color4.White
            };

            yield return new OsuSpriteText
            {
                Text = $"{score.Statistics[HitResult.Miss]}",
                Font = OsuFont.GetFont(size: TEXT_SIZE),
                Colour = score.Statistics[HitResult.Miss] == 0 ? Color4.Gray : Color4.White
            };
        }

        protected override Drawable CreatePpCell() => new OsuSpriteText
        {
            Text = $@"{score.PP:N0}",
            Font = OsuFont.GetFont(size: TEXT_SIZE)
        };

        protected override Drawable CreateModsCell() => new FillFlowContainer
        {
            Direction = FillDirection.Horizontal,
            AutoSizeAxes = Axes.Both,
            ChildrenEnumerable = score.Mods.Select(m => new ModIcon(m)
            {
                AutoSizeAxes = Axes.Both,
                Scale = new Vector2(0.3f)
            })
        };

        private class ClickableScoreUsername : ClickableUserContainer
        {
            private const int fade_duration = 100;

            private readonly SpriteText text;
            private readonly SpriteText textBold;

            public ClickableScoreUsername()
            {
                Add(text = new OsuSpriteText
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Font = OsuFont.GetFont(size: TEXT_SIZE)
                });

                Add(textBold = new OsuSpriteText
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Font = OsuFont.GetFont(size: TEXT_SIZE, weight: FontWeight.Bold),
                    Alpha = 0,
                });
            }

            protected override void OnUserChange(User user)
            {
                text.Text = textBold.Text = user.Username;
            }

            protected override bool OnHover(HoverEvent e)
            {
                textBold.FadeIn(fade_duration, Easing.OutQuint);
                text.FadeOut(fade_duration, Easing.OutQuint);
                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                textBold.FadeOut(fade_duration, Easing.OutQuint);
                text.FadeIn(fade_duration, Easing.OutQuint);
                base.OnHoverLost(e);
            }
        }
    }
}
