// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Core;

public record AssignWorkflowRequest(Configuration Configuration, bool Commit) : DefaultWorkflowRequest(Configuration);