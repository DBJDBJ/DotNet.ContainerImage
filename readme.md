https://learn.microsoft.com/en-gb/dotnet/core/docker/publish-as-container

Publish into an docker image:
```bat
dotnet publish --os linux --arch x64 /t:PublishContainer -c Release 
```

## Roadmap

- use the TCP port
  - there are many advice against [web socket](https://learn.microsoft.com/en-us/dotnet/api/system.net.websockets.clientwebsocket?view=net-7.0)
  - consider JSON-RPC, too
- pass the SAS out 


---

| French     | English                                                                         |
| ---------- | ------------------------------------------------------------------------------- |
| **sasser** | sieve [sieved, sieved, sieving, sieves] (to strain, sift or sort using a sieve) |
