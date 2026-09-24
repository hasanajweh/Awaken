# DisplayKeep

A zero-interaction Windows utility whose only job is to keep the system and
display awake while it is running.

## Privacy and behavior

DisplayKeep:

- makes no network requests;
- collects and sends no telemetry or analytics;
- contains no application-level file I/O;
- contains no registry I/O;
- displays no window, tray icon, notification, or console;
- uses no polling loop and performs no work while waiting;
- allows only one instance per Windows session.

The program calls the documented Windows `SetThreadExecutionState` API with
`ES_CONTINUOUS`, `ES_SYSTEM_REQUIRED`, and `ES_DISPLAY_REQUIRED`, then sleeps
indefinitely. Closing the process restores normal Windows power behavior.

It does not simulate keyboard or mouse input, change power-plan settings,
prevent a user-requested shutdown, or bypass an organization-enforced lock
policy.

## Build without downloads

On 64-bit Windows, run:

```bat
build.cmd
```

The build uses the .NET Framework compiler included with Windows. It does not
restore packages, contact NuGet, or require an SDK installation. The output is
`publish\DisplayKeep.exe`.

## Run and stop

Double-click `DisplayKeep.exe` to start it. It intentionally has no interface.
Start it a second time and the duplicate exits immediately.

To stop it, end `DisplayKeep.exe` in Task Manager. Windows automatically clears
the execution-state request when the process ends.

If automatic startup is allowed by your organization, place a shortcut to the
executable in your personal Startup folder. DisplayKeep itself does not alter
startup settings.
