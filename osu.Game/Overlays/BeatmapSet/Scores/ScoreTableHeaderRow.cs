// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Overlays.BeatmapSet.Scores
{
    public class ScoreTableHeaderRow : ScoreTableRow
    {
        protected override Drawable CreateIndexCell() => new CellText("rank");

        protected override Drawable CreateRankCell() => new Container();

        protected override Drawable CreateScoreCell() => new CellText("score");

        protected override Drawable CreateAccuracyCell() => new CellText("accuracy");

        protected override Drawable CreatePlayerCell() => new CellText("player");

        protected override IEnumerable<Drawable> CreateStatisticsCells() => new[]
        {
            new CellText("max combo"),
            new CellText("300"),
            new CellText("100"),
            new CellText("50"),
            new CellText("miss"),
        };

        protected override Drawable CreatePpCell() => new CellText("pp");

        protected override Drawable CreateModsCell() => new CellText("mods");

        private class CellText : OsuSpriteText
        {
            public CellText(string text)
            {
                Text = text.ToUpper();
                Font = OsuFont.GetFont(size: 12, weight: FontWeight.Black);
            }
        }
    }
}
