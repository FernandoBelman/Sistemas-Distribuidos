
FROM golang:1.22.6-alpine AS base

WORKDIR /app
EXPOSE 80

# Construir
FROM --platform=$BUILDPLATFORM golang:1.22.6-alpine AS build
ARG configuration=release
WORKDIR /src

COPY go.mod ./

# Descarga go mod
RUN go mod download

# Copia los demas archivos
COPY . .

RUN go build -o /app/server .


FROM base AS final
WORKDIR /app
COPY --from=build /app/server .


ENTRYPOINT ["./server"]
