/*
    EnviousBox - Caesar
    Copyright (C) 2024 Alastair Lundy

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.Reflection;

using AlastairLundy.Extensions.System;

using Caesar.Commands;
using Caesar.localizations;

using Spectre.Console.Cli;

CommandApp app = new CommandApp();

app.Configure(config =>
{
    config.AddBranch<CommandSettings>("encode", add =>
    {
        add.AddCommand<EncodeCommand>("string")
            .WithAlias("")
            .WithDescription(Resources.Command_Encode_Label);
        
        add.AddCommand<EncodeFileCommand>("file");
    });
    
    config.AddBranch<CommandSettings>("decode", add =>
    {
        add.AddCommand<DecodeCommand>("string")
            .WithAlias("")
            .WithDescription(Resources.Command_Decode_Label);
        
        add.AddCommand<DecodeFileCommand>("file");
    });

    
    config.SetApplicationVersion(Assembly.GetExecutingAssembly().GetName().Version.ToFriendlyVersionString());
});

return app.Run(args);