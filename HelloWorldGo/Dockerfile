FROM golang:1.22.6-alpine AS base
WORKDIR /app
EXPOSE 80
ENV GIN_MODE=release
USER app

FROM --platform=$BUILDPLATFORM golang:1.22.6-alpine AS build
ARG configuration=Release
WORKDIR /src
COPY ["go.mod", "./"]
RUN go mod download
COPY . .
WORKDIR "/src/"
RUN go build -o /app/build/main main.go

FROM build as publish
ARG configuration=Release
WORKDIR /app/publish
RUN cp /app/build/main .

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./main"]
