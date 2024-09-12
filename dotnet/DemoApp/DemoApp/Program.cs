// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


/* build this like we are building an agent
 * One solution multiple projects
 * 1. create the kernel and make a user message streaming and invoke -- Jason
 * 2. System prompt? few system prompts. you are a fan now answer if you are a cubs announcer etc -- Jason
 * 3. Add history? -- Pete
 * 4. Create plugin? - Plugin (Pete will develop only the plug and register it)
 *      -- Semantic Plugin
 *      -- Native Plugin
 * 5. Filters for retry and guardrails for prompt filter -- Tyler (Make a filter that doesn't like a team and removes them)
 * 6. Agent -- Tyler (Create on the agent and use the kernel code to create the agent)
 * 7. Agent Group Chat -- Pete Use the agent 1 agent who is a fan of a team and can view get the schedule with a plugin that buys tickets and the second agent validates that the 
 *      ticket is not on a day you are busy and approves or denies
 * 
 * 
 * later lets do handlebars
 */