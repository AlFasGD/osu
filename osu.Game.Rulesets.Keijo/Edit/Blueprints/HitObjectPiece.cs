﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Keijo.Objects;
using osuTK;

namespace osu.Game.Rulesets.Keijo.Edit.Blueprints
{
    /// <summary>
    /// A piece of a blueprint which responds to changes in the state of a <see cref="KeijoHitObject"/>.
    /// </summary>
    public abstract class HitObjectPiece : CompositeDrawable
    {
        protected readonly IBindable<Vector2> PositionBindable = new Bindable<Vector2>();
        protected readonly IBindable<float> ScaleBindable = new Bindable<float>();

        private readonly KeijoHitObject hitObject;

        protected HitObjectPiece(KeijoHitObject hitObject)
        {
            this.hitObject = hitObject;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            PositionBindable.BindTo(hitObject.PositionBindable);
            ScaleBindable.BindTo(hitObject.ScaleBindable);
        }
    }
}
